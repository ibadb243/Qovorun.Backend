using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Queries.GetDetailsQuery;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsVm>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IShortnameDbContext _shortnameDbContext;
    private readonly IMapper _mapper;

    public GetUserDetailsQueryHandler(IUserDbContext userDbContext, IShortnameDbContext shortnameDbContext, IMapper mapper)
    {
        _userDbContext = userDbContext;
        _shortnameDbContext = shortnameDbContext;
        _mapper = mapper;
    }

    public async Task<UserDetailsVm> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
         var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
         if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");

         var user = requester;
         if (request.RequesterId != request.UserId)
         {
             user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
             if (user == null || user.DeletedAt != null) throw new Exception("User not found");
         }
         
         var shortname = await _shortnameDbContext.Shortnames.FirstAsync(s => s.Owner == ShortnameOwner.User && s.OwnerId == user.Id, cancellationToken)!;
         
         var vm = _mapper.Map<UserDetailsVm>(user);
         vm.Shortname = shortname.Shortname;
         
         return vm;
   }
}
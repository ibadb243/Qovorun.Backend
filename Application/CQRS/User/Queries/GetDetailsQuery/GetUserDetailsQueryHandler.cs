using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Queries.GetDetailsQuery;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsVm>
{
    private readonly IUserService _userService;
    private readonly IShortnameService _shortnameService;
    private readonly IMapper _mapper;

    public GetUserDetailsQueryHandler(IUserService userService, IShortnameService shortnameService, IMapper mapper)
    {
        _userService = userService;
        _shortnameService = shortnameService;
        _mapper = mapper;
    }

    public async Task<UserDetailsVm> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
         var user = await _userService.GetUserAsync(request.UserId, cancellationToken);
         if (user == null) throw new Exception("User not found");
         
         var shortname = await _shortnameService.GetShortnameAsync(ShortnameOwner.User, user.Id, cancellationToken);
         
         var vm = _mapper.Map<UserDetailsVm>(user);
         vm.Shortname = shortname!.Shortname;
         
         return vm;
   }
}
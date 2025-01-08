using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands.UpdateCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IShortnameDbContext _shortnameDbContext;
    
    public UpdateUserCommandHandler(IUserDbContext userDbContext, IShortnameDbContext shortnameDbContext)
    {
        _userDbContext = userDbContext;
        _shortnameDbContext = shortnameDbContext;
    }
    
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");
        
        requester.Firstname = request.Firstname;
        requester.Lastname = request.Lastname;
        requester.Description = request.Description;
        
        requester.UpdatedAt = DateTimeOffset.UtcNow;
        
        await _userDbContext.SaveChangesAsync(cancellationToken);
    }
}
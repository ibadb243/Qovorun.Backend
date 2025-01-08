using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands.DeleteCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IShortnameDbContext _shortnameDbContext;

    public DeleteUserCommandHandler(IUserDbContext userDbContext, IShortnameDbContext shortnameDbContext)
    {
        _userDbContext = userDbContext;
        _shortnameDbContext = shortnameDbContext;
    }
    
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");

        var shortname = await _shortnameDbContext.Shortnames.FirstAsync(s => s.Owner == ShortnameOwner.User && s.OwnerId == requester.Id, cancellationToken)!;

        shortname.Owner = ShortnameOwner.None;
        requester.DeletedAt = DateTimeOffset.UtcNow;
        
        await _userDbContext.SaveChangesAsync(cancellationToken);
        await _shortnameDbContext.SaveChangesAsync(cancellationToken);
    }
}
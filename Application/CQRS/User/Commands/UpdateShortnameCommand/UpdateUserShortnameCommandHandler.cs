using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands.UpdateShortnameCommand;

public class UpdateUserShortnameCommandHandler : IRequestHandler<UpdateUserShortnameCommand>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IShortnameDbContext _shortnameDbContext;

    public UpdateUserShortnameCommandHandler(IUserDbContext userDbContext, IShortnameDbContext shortnameDbContext)
    {
        _userDbContext = userDbContext;
        _shortnameDbContext = shortnameDbContext;
    }
    
    public async Task Handle(UpdateUserShortnameCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");

        var shortname = await _shortnameDbContext.Shortnames.FirstAsync(s => s.Owner == ShortnameOwner.User && s.OwnerId == requester.Id, cancellationToken)!;
        if (shortname.Shortname == request.Shortname) return;
        
        var new_shortname = await _shortnameDbContext.Shortnames.FirstOrDefaultAsync(s => s.Shortname == request.Shortname, cancellationToken);
        if (new_shortname != null && new_shortname.Owner != ShortnameOwner.None) throw new Exception("The short name is already taken");
        
        if (new_shortname == null)
        {
            new_shortname = new ShortnameField
            {
                Id = Guid.NewGuid(),
                Owner = ShortnameOwner.User,
                OwnerId = requester.Id,
                Shortname = request.Shortname,
                CreatedAt = DateTimeOffset.UtcNow,
            };
            
            await _shortnameDbContext.Shortnames.AddAsync(new_shortname, cancellationToken);
        }
        else
        {
            new_shortname.Owner = ShortnameOwner.User;
            new_shortname.OwnerId = requester.Id;
            new_shortname.ChangedOwnerAt = DateTimeOffset.UtcNow;
        }

        shortname.Owner = ShortnameOwner.None;
        shortname.ChangedOwnerAt = DateTimeOffset.UtcNow;

        await _shortnameDbContext.SaveChangesAsync(cancellationToken);
    }
}
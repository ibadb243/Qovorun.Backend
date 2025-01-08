using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;

namespace Application.CQRS.User.Commands.CreateCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IShortnameDbContext _shortnameDbContext;

    public CreateUserCommandHandler(IUserDbContext userDbContext, IShortnameDbContext shortnameDbContext)
    {
        _userDbContext = userDbContext;
        _shortnameDbContext = shortnameDbContext;
    }
    
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var phoneUser = await _userDbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);
        if (phoneUser != null) throw new Exception("The Phone number is already in use.");
        
        var shortname = await _shortnameDbContext.Shortnames.FirstOrDefaultAsync(s => s.Shortname == request.Shortname, cancellationToken);
        if (shortname != null && shortname.Owner != ShortnameOwner.None) throw new Exception("The short name is already in use.");

        string hashedPassword;
        using (var hmac = new HMACSHA512())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(request.Password);
            var hashBytes = hmac.ComputeHash(passwordBytes);
            
            hashedPassword = Convert.ToBase64String(hashBytes);
        }
        
        var user = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Description = request.Description,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = hashedPassword,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        
        if (shortname == null)
        {
            shortname = new ShortnameField
            {
                Id = Guid.NewGuid(),
                Owner = ShortnameOwner.User,
                OwnerId = user.Id,
                Shortname = request.Shortname,
                CreatedAt = DateTimeOffset.UtcNow,
            };
            
            await _shortnameDbContext.Shortnames.AddAsync(shortname, cancellationToken);
        }
        else
        {
            shortname.ChangedOwnerAt = DateTimeOffset.UtcNow;
            shortname.Owner = ShortnameOwner.User;
            shortname.OwnerId = user.Id;
        }
        
        await _userDbContext.Users.AddAsync(user, cancellationToken);
        await _userDbContext.SaveChangesAsync(cancellationToken);
        
        await _shortnameDbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
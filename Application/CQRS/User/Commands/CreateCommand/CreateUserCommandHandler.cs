using System.Security.Cryptography;
using System.Text;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;

namespace Application.CQRS.User.Commands.CreateCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserService _userService;
    private readonly IShortnameService _shortnameService;

    public CreateUserCommandHandler(IUserService userService, IShortnameService shortnameService)
    {
        _userService = userService;
        _shortnameService = shortnameService;
    }
    
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isFree = await _userService.IsPhoneNumberFreeAsync(request.PhoneNumber, cancellationToken);
        if (!isFree) throw new Exception("Phone number is used");
        
        isFree = await _shortnameService.IsShortnameFreeAsync(request.Shortname, cancellationToken);
        if (!isFree) throw new Exception("Shortname is used");
        
        var user = await _userService.CreateUserAsync(
            request.Firstname, 
            request.Lastname, 
            request.Description, 
            request.PhoneNumber, 
            request.Password, 
            cancellationToken);

        var shortname = await _shortnameService.GetShortnameAsync(request.Shortname, cancellationToken);
        
        if (shortname == null) 
            await _shortnameService.CreateShortnameAsync(request.Shortname, ShortnameOwner.User, user.Id, cancellationToken);
        else 
            await _shortnameService.UpdateShortnameAsync(request.Shortname, ShortnameOwner.User, user.Id, cancellationToken);

        return user.Id;
    }
}
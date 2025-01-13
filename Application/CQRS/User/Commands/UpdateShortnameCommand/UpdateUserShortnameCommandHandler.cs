using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands.UpdateShortnameCommand;

public class UpdateUserShortnameCommandHandler : IRequestHandler<UpdateUserShortnameCommand>
{
    private readonly IUserService _userService;
    private readonly IShortnameService _shortnameService;

    public UpdateUserShortnameCommandHandler(IUserService userService, IShortnameService shortnameService)
    {
        _userService = userService;
        _shortnameService = shortnameService;
    }
    
    public async Task Handle(UpdateUserShortnameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync(request.RequesterId, cancellationToken);
        if (user == null) throw new Exception("User not found");
        
        var isFree = await _shortnameService.IsShortnameFreeAsync(request.Shortname, cancellationToken);
        if (!isFree) throw new Exception("Shortname is used");
        
        var shortname = await _shortnameService.GetShortnameAsync(request.Shortname, cancellationToken);
        
        if (shortname == null) 
            await _shortnameService.CreateShortnameAsync(request.Shortname, ShortnameOwner.User, user.Id, cancellationToken);
        else 
            await _shortnameService.UpdateShortnameAsync(request.Shortname, ShortnameOwner.User, user.Id, cancellationToken);
    }
}
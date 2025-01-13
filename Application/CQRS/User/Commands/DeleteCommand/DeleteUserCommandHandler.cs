using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands.DeleteCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserService _userService;
    private readonly IShortnameService _shortnameService;

    public DeleteUserCommandHandler(IUserService userService, IShortnameService shortnameService)
    {
        _userService = userService;
        _shortnameService = shortnameService;
    }
    
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync(request.RequesterId, cancellationToken);
        if (user == null) throw new Exception("User not found");
        
        await _userService.DeleteUserAsync(user.Id, cancellationToken);
        await _shortnameService.ClearShortnameAsync(ShortnameOwner.User, user.Id, cancellationToken);
    }
}
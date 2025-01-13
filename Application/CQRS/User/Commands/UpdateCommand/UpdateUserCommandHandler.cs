using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands.UpdateCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserService _userService;
    
    public UpdateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _userService.UpdateUserAsync(
            request.RequesterId, 
            request.Firstname, 
            request.Lastname, 
            request.Description,
            cancellationToken);
    }
}
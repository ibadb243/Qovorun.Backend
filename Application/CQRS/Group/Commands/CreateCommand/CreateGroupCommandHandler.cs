using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands.CreateCommand;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    private readonly IMembersService _membersService;
    private readonly IShortnameService _shortnameService;
    
    public CreateGroupCommandHandler(
        IUserService userService,
        IGroupService groupService,
        IMembersService membersService,
        IShortnameService shortnameService)
    {
        _userService = userService;
        _groupService = groupService;
        _membersService = membersService;
        _shortnameService = shortnameService;
    }

    public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userService.GetUserAsync(request.RequesterId, cancellationToken);
        if (requester == null) throw new Exception("User not found");

        if (!await _shortnameService.IsShortnameFreeAsync(request.Shortname, cancellationToken)) throw new Exception("the shortname is already taken");
        
        var group = await _groupService.CreateGroupAsync(request.Name, request.Description, cancellationToken);
        
        var shortnameF = await _shortnameService.GetShortnameAsync(request.Shortname, cancellationToken);
        
        if (shortnameF == null)
            await _shortnameService.CreateShortnameAsync(request.Shortname, ShortnameOwner.Group, group.Id, cancellationToken);
        else
            await _shortnameService.UpdateShortnameAsync(request.Shortname, ShortnameOwner.Group, shortnameF.Id, cancellationToken);

        await _membersService.CreateOwnerAsync(requester.Id, group.Id, cancellationToken);
        
        return group.Id;
    }
}
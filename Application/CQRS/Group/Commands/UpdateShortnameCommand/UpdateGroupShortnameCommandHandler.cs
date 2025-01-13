using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Group.Commands.UpdateShortnameCommand;

public class UpdateGroupShortnameCommandHandler : IRequestHandler<UpdateGroupShortnameCommand>
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    private readonly IMembersService _memberService;
    private readonly IShortnameService _shortnameService;
    
    public UpdateGroupShortnameCommandHandler(
        IUserService userService,
        IGroupService groupService,
        IMembersService memberService,
        IShortnameService shortnameService)
    {
        _userService = userService;
        _groupService = groupService;
        _memberService = memberService;
        _shortnameService = shortnameService;
    }
    
    public async Task Handle(UpdateGroupShortnameCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userService.GetUserAsync(request.RequesterId, cancellationToken);
        if (requester == null) throw new Exception("User not found");
        
        var group = await _groupService.GetGroupAsync(request.GroupId, cancellationToken);
        if (group == null) throw new Exception("Group not found");
        
        var member = await _memberService.GetMemberAsync(requester.Id, group.Id, cancellationToken);
        if (member == null) throw new Exception("You are not a member of this group");

        if (!await _shortnameService.IsShortnameFreeAsync(request.Shortname, cancellationToken))
            throw new Exception("Shortname is not free");
        
        var shortnameF = await _shortnameService.GetShortnameAsync(request.Shortname, cancellationToken);

        if (shortnameF == null)
            await _shortnameService.CreateShortnameAsync(request.Shortname, ShortnameOwner.Group, group.Id, cancellationToken);
        else
        {
            await _shortnameService.ClearShortnameAsync(request.Shortname, cancellationToken);
            await _shortnameService.UpdateShortnameAsync(request.Shortname, ShortnameOwner.Group, group.Id, cancellationToken);
        }
    }
}
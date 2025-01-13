using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Group.Commands.UpdateCommand;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand>
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    private readonly IMembersService _memberService;
    private readonly IShortnameService _shortnameService;
    
    public UpdateGroupCommandHandler(
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
    
    public async Task Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userService.GetUserAsync(request.RequesterId, cancellationToken);
        if (requester == null) throw new Exception("User not found");
        
        var group = await _groupService.GetGroupAsync(request.GroupId, cancellationToken);
        if (group == null) throw new Exception("Group not found");
        
        var member = await _memberService.GetMemberAsync(requester.Id, group.Id, cancellationToken);
        if (member == null) throw new Exception("You are not a member of this group");

        await _groupService.UpdateGroupAsync(group.Id, request.Name, request.Description, cancellationToken);
    }
}
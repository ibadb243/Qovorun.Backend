using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Member.Commands.UpdateCommand;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand>
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    private readonly IMembersService _memberService;
    
    public UpdateMemberCommandHandler(
        IUserService userService,
        IGroupService groupService,
        IMembersService memberService)
    {
        _userService = userService;
        _groupService = groupService;
        _memberService = memberService;
    }
    
    public async Task Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userService.GetUserAsync(request.RequesterId, cancellationToken);
        if (requester == null) throw new Exception("User not found");
        
        var member = await _memberService.GetMemberAsync(request.MemberId, cancellationToken);
        if (member == null) throw new Exception("Member not found");

        var requesterMember = await _memberService.GetMemberAsync(requester.Id, member.GroupId, cancellationToken);
        if (requesterMember == null) throw new Exception("Member not found");

        if (!requesterMember.Permissions.HasFlag(GroupMemberPermission.CanManageRoles)) throw new Exception("Can't manage roles");
        
        await _memberService.UpdateMemberAsync(member.Id, member.Role, member.Permissions, request.Nickname, cancellationToken);
    }
}
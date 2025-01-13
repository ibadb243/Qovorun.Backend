using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Member.Commands.DeleteCommand;

public class BanMemberCommandHandler : IRequestHandler<BanMemberCommand>
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    private readonly IMembersService _memberService;
    
    public BanMemberCommandHandler(
        IUserService userService,
        IGroupService groupService,
        IMembersService memberService)
    {
        _userService = userService;
        _groupService = groupService;
        _memberService = memberService;
    }
    
    public async Task Handle(BanMemberCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userService.GetUserAsync(request.RequesterId, cancellationToken);
        if (requester == null) throw new Exception("User not found");
        
        var member = await _memberService.GetMemberAsync(request.MemberId, cancellationToken);
        if (member == null) throw new Exception("Member not found");

        var requesterMember = await _memberService.GetMemberAsync(requester.Id, member.GroupId, cancellationToken);
        if (requesterMember == null) throw new Exception("Member not found");

        if (!requesterMember.Permissions.HasFlag(GroupMemberPermission.CanBanMembers)) throw new Exception("You do not have permission to ban members");
        
        await _memberService.BanMemberAsync(member.Id, cancellationToken);
    }
}
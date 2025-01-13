using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Member.Commands.CreateCommand;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Guid>
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    private readonly IMembersService _memberService;
    
    public CreateMemberCommandHandler(
        IUserService userService,
        IGroupService groupService,
        IMembersService memberService)
    {
        _userService = userService;
        _groupService = groupService;
        _memberService = memberService;
    }
    
    public async Task<Guid> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userService.GetUserAsync(request.RequesterId, cancellationToken);
        if (requester == null) throw new Exception("User not found");
        
        var user = await _userService.GetUserAsync(request.UserId, cancellationToken);
        if (user == null) throw new Exception("User not found");
        
        var group = await _groupService.GetGroupAsync(request.GroupId, cancellationToken);
        if (group == null) throw new Exception("Group not found");

        var member = await _memberService.CreateMemberAsync(user.Id, group.Id, cancellationToken);
        
        return member.Id;
    }
}
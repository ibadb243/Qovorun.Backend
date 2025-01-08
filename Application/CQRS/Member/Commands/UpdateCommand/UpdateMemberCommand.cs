using Domain;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Member.Commands.UpdateCommand;

public class UpdateMemberCommand : IRequest
{
    public Guid RequesterId { get; set; }
    public Guid MemberId { get; set; }
    public string Nickname { get; set; }
}
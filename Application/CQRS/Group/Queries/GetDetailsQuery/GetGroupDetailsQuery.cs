using MediatR;

namespace Application.CQRS.Group.Queries.GetDetailsQuery;

public class GetGroupDetailsQuery : IRequest<GroupDetailsVm>
{
    public Guid RequesterId { get; set; }
    public Guid GroupId { get; set; }
}
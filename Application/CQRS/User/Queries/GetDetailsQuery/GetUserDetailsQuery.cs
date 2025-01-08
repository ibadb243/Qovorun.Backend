using MediatR;

namespace Application.CQRS.User.Queries.GetDetailsQuery;

public class GetUserDetailsQuery : IRequest<UserDetailsVm>
{
    public Guid RequesterId { get; set; }
    public Guid UserId { get; set; }
}
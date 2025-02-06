using FluentValidation;

namespace Application.CQRS.Group.Queries.GetDetailsQuery;

public class GetGroupDetailsQueryValidator : AbstractValidator<GetGroupDetailsQuery>
{
    public GetGroupDetailsQueryValidator()
    {
        RuleFor(q => q.RequesterId).NotEqual(Guid.Empty);
        RuleFor(q => q.GroupId).NotEqual(Guid.Empty);
    }
}
namespace Application.Common.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid userId)
        : base($"User {userId} not found.") { }
}
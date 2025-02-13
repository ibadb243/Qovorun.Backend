namespace Application.Common.Exceptions;

public class ShortnameAlreadyTakenException : Exception
{
    public ShortnameAlreadyTakenException(string shortname)
        : base($"Shortname {shortname} is already taken.") { }
}
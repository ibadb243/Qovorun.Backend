namespace Application.Common.Exceptions;

public class PhoneNumberAlreadyTakenException : Exception
{
    public PhoneNumberAlreadyTakenException(string phoneNumber) 
        : base($"Phone number {phoneNumber} already taken.") { }
}
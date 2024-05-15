namespace Banks.Tools;

public class PhoneNumberException : Exception
{
    private PhoneNumberException(string message)
        : base(message)
    {
    }

    public static PhoneNumberException PhoneNumberIsNullException()
    {
        return new PhoneNumberException("Phone number is null!");
    }

    public static PhoneNumberException PhoneNumberIsInvalidException()
    {
        return new PhoneNumberException("Phone number is invalid!");
    }
}
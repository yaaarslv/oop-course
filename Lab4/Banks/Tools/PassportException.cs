namespace Banks.Tools;

public class PassportException : Exception
{
    private PassportException(string message)
        : base(message) { }

    public static PassportException PassportDataIsNullException()
    {
        return new PassportException("Passport data is null!");
    }

    public static PassportException PassportDataIsNotFullException()
    {
        return new PassportException("Passport data isn't full!");
    }
}
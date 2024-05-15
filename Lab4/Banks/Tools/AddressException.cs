namespace Banks.Tools;

public class AddressException : Exception
{
    private AddressException(string message)
        : base(message)
    {
    }

    public static AddressException AddressIsNullException()
    {
        return new AddressException("Address is null!");
    }

    public static AddressException AddressIsNotFullException()
    {
        return new AddressException("Address isn't full!");
    }
}
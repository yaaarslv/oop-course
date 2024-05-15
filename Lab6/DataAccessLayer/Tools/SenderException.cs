namespace DataAccessLayer.Tools;

public class SenderException : Exception
{
    private SenderException(string message)
        : base(message) { }

    public static SenderException SenderIsNullException()
    {
        return new SenderException("Sender is null!");
    }

    public static SenderException SenderInfoIsNull()
    {
        return new SenderException("Sender info is null!");
    }
}
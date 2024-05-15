namespace DataAccessLayer.Tools;

public class MessageException : Exception
{
    private MessageException(string message)
        : base(message) { }

    public static MessageException MessageIsNullException()
    {
        return new MessageException("Message is null!");
    }

    public static MessageException TextIsNullException()
    {
        return new MessageException("Message text is null!");
    }

    public static MessageException ActionAlreadyDoneException()
    {
        return new MessageException("This action has already done!");
    }

    public static MessageException MessageAlreadyExistsException()
    {
        return new MessageException("Message already exists in list!");
    }

    public static MessageException MessageNotExistsException()
    {
        return new MessageException("Message doesn't exist in list of messages!");
    }
}

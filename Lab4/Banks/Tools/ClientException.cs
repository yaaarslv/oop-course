namespace Banks.Tools;

public class ClientException : Exception
{
    private ClientException(string message)
        : base(message)
    {
    }

    public static ClientException ClientIsNullException()
    {
        return new ClientException("Client is null!");
    }

    public static ClientException ClientNameIsNullException()
    {
        return new ClientException("Client name is null!");
    }

    public static ClientException ClientAlreadyExistsException()
    {
        return new ClientException("Client already exists in list!");
    }

    public static ClientException ClientNotExistsException()
    {
        return new ClientException("Client doesn't exist in list of bank clients!");
    }

    public static ClientException NameIsNotFullException()
    {
        return new ClientException("Name isn't full!");
    }
}
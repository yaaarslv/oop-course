namespace Banks.Tools;

public class BankException : Exception
{
    private BankException(string message)
        : base(message)
    {
    }

    public static BankException BankIsNullException()
    {
        return new BankException("Bank is null!");
    }

    public static BankException BankNameIsNullException()
    {
        return new BankException("Bank name is null!");
    }

    public static BankException BankAlreadyCreatedException()
    {
        return new BankException("Bank is already created!");
    }
}

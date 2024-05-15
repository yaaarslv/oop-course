namespace Banks.Tools;

public class BankAccountException : Exception
{
    private BankAccountException(string message)
        : base(message) { }

    public static BankAccountException BankAccountIsNullException()
    {
        return new BankAccountException("Bank account is null!");
    }

    public static BankAccountException BankAccountAlreadyExistsException()
    {
        return new BankAccountException("Bank account already exists in list of bank accounts!");
    }

    public static BankAccountException BankAccountNotExistsException()
    {
        return new BankAccountException("Bank account doesn't exist in list of bank accounts!");
    }

    public static BankAccountException OperationLimitForDoubtfulExceeded()
    {
        return new BankAccountException("Operation limit for doubt bank account exceeded!");
    }

    public static BankAccountException OperationLimitExceeded()
    {
        return new BankAccountException("Operation limit exceeded!");
    }

    public static BankAccountException PercentAlreadyContains()
    {
        return new BankAccountException("Percent already exists!");
    }

    public static BankAccountException PercentNotExist()
    {
        return new BankAccountException("Percent doesn't exist!");
    }
}
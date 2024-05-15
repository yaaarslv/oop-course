namespace Banks.Tools;

public class TransactionException : Exception
{
    private TransactionException(string message)
        : base(message) { }

    public static TransactionException InvalidMoneyException()
    {
        return new TransactionException("Money is invalid!");
    }

    public static TransactionException TransactionIsNullException()
    {
        return new TransactionException("Transaction is null!");
    }

    public static TransactionException TransactionIsAlreadyCancelledException()
    {
        return new TransactionException("Transaction is already cancelled!");
    }

    public static TransactionException NoEnoughMoneyException()
    {
        return new TransactionException("There is not enough money on bank account!");
    }

    public static TransactionException TimeHasNotExpired()
    {
        return new TransactionException("Finish time hasn't expired yet");
    }
}
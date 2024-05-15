using Banks.Tools;

namespace Banks.Models;

public class Transaction
{
    public Transaction(DateTime transactionTime, Guid sender, Guid recipient, decimal money, decimal commission, string transactionType)
    {
        if (money <= 0 || commission < 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        TransactionTime = transactionTime;
        Sender = sender;
        Recipient = recipient;
        Money = money;
        Commission = commission;
        IsCancelled = false;
        TransactionType = transactionType;
    }

    public DateTime TransactionTime { get; }
    public Guid Sender { get; }
    public Guid Recipient { get; }
    public decimal Money { get; }
    public decimal Commission { get; }
    public bool IsCancelled { get; private set; }
    public string TransactionType { get; }

    public void ChangeIsCancelledFlag()
    {
        IsCancelled = !IsCancelled;
    }
}
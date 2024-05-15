using Banks.Models;
using Banks.Tools;

namespace Banks.Entities;

public class CreditAccount : IBankAccount
{
    private readonly List<Transaction> _transactions;

    public CreditAccount(Bank bank, Client client, decimal startMoney)
    {
        if (bank is null)
        {
            throw BankException.BankIsNullException();
        }

        if (client is null)
        {
            throw ClientException.ClientIsNullException();
        }

        if (startMoney <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        if (startMoney > Bank?.CreditLimit)
        {
            throw BankAccountException.OperationLimitExceeded();
        }

        Client = client;
        Bank = bank;
        _transactions = new List<Transaction>();
        Id = Guid.NewGuid();
        StartMoney = startMoney;
        Money = StartMoney;
        IsDoubtful = true;
        CreationDateTime = DateTime.Now;
        FinishDateTime = CreationDateTime.AddYears(1);
    }

    public IReadOnlyCollection<Transaction> Transactions => _transactions;
    public Client Client { get; }
    public Bank Bank { get; }
    public decimal StartMoney { get; }
    public decimal Money { get; private set; }
    public Guid Id { get; }
    public bool IsDoubtful { get; private set; }
    public DateTime CreationDateTime { get; }
    public DateTime FinishDateTime { get; }

    public void Replenish(decimal money)
    {
        if (money <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        var commission = Money - money < StartMoney ? Bank.BankPercents.Commission : 0;
        Money += money - commission;
        var newTransaction = new Transaction(DateTime.Now, Id, Id, money, commission, "replenish");
        _transactions.Add(newTransaction);
    }

    public void Withdraw(decimal money)
    {
        if (money <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        if (Money < money)
        {
            throw TransactionException.NoEnoughMoneyException();
        }

        if (CheckAccountLimit(money))
        {
            throw BankAccountException.OperationLimitForDoubtfulExceeded();
        }

        var commission = Money - money < StartMoney ? Bank.BankPercents.Commission : 0;
        Money -= money + commission;
        var newTransaction = new Transaction(DateTime.Now, Id, Id, money, commission, "withdraw");
        _transactions.Add(newTransaction);
    }

    public void Transfer(Guid recipient, decimal money)
    {
        if (money <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        if (Money < money)
        {
            throw TransactionException.NoEnoughMoneyException();
        }

        if (CheckAccountLimit(money))
        {
            throw BankAccountException.OperationLimitForDoubtfulExceeded();
        }

        if (CheckTransferLimit(money))
        {
            throw BankAccountException.OperationLimitExceeded();
        }

        var recipientBankAccount = Bank.Clients.SelectMany(c => c.BankAccounts).SingleOrDefault(b => b.Id == recipient);
        var commission = Money - money < StartMoney ? Bank.BankPercents.Commission : 0;
        Withdraw(money + commission);
        recipientBankAccount?.Replenish(money);
        var newTransaction = new Transaction(DateTime.Now, Id, recipient, money, commission, "transfer");
        _transactions.Add(newTransaction);
    }

    public void ChangeDoubtToConfirmed()
    {
        IsDoubtful = false;
    }

    public string PrintAccountData()
    {
        return $"Money: {Money}, StartMoney:{StartMoney}, CreationDateTime: {CreationDateTime}, FinishDateTime: {FinishDateTime}, Id: {Id}, Bank: {Bank.Name}";
    }

    private bool CheckAccountLimit(decimal money)
    {
        return IsDoubtful && money > Bank.LimitForDoubtful;
    }

    private bool CheckTransferLimit(decimal money)
    {
        return money > Bank.Limit;
    }
}
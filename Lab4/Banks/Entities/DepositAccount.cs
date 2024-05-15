using Banks.Models;
using Banks.Tools;

namespace Banks.Entities;

public class DepositAccount : IBankAccount
{
    private const int DaysInYear = 365;
    private const int PercentCoefficient = 100;
    private readonly List<Transaction> _transactions;
    private decimal _percentBalance;

    public DepositAccount(Bank bank, Client? client, decimal startMoney)
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

        Client = client;
        Bank = bank;
        _transactions = new List<Transaction>();
        Id = Guid.NewGuid();
        StartMoney = startMoney;
        Money = StartMoney;
        _percentBalance = 0;
        CreationDateTime = DateTime.Now;
        FinishDateTime = CreationDateTime.AddYears(1);
    }

    public IReadOnlyCollection<Transaction> Transactions => _transactions;
    public Client? Client { get; }
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

        Money += money;
        var newTransaction = new Transaction(DateTime.Now, Id, Id, money, 0, "replenish");
        _transactions.Add(newTransaction);
    }

    public void Withdraw(decimal money)
    {
        if (DateTime.Now < FinishDateTime)
        {
            throw TransactionException.TimeHasNotExpired();
        }

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

        Money -= money;
        var newTransaction = new Transaction(DateTime.Now, Id, Id, money, 0, "withdraw");
        _transactions.Add(newTransaction);
    }

    public void Transfer(Guid recipient, decimal money)
    {
        if (DateTime.Now < FinishDateTime)
        {
            throw TransactionException.TimeHasNotExpired();
        }

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
        Withdraw(money);
        recipientBankAccount?.Replenish(money);
        var newTransaction = new Transaction(DateTime.Now, Id, recipient, money, 0, "transfer");
        _transactions.Add(newTransaction);
    }

    public void ChangeDoubtToConfirmed()
    {
        IsDoubtful = false;
    }

    public void AccruePercents()
    {
        var percent = Bank.BankPercents.DepositPercents.SingleOrDefault(p => p.Key.Key <= StartMoney && p.Key.Value >= StartMoney)
            .Value;
        _percentBalance += Money * percent / DaysInYear / PercentCoefficient;
    }

    public void AddPercentsToBalance()
    {
        Money += _percentBalance;
        _percentBalance = 0;
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
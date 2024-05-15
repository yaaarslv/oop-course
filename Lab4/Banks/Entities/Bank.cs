using Banks.Models;
using Banks.Tools;

namespace Banks.Entities;

public class Bank
{
    private readonly List<Client> _clients;
    private readonly List<Client> _subscribedToNotify;
    private readonly List<DebitAccount> _debitAccounts;
    private readonly List<DepositAccount> _depositAccounts;
    private readonly List<CreditAccount> _creditAccounts;
    private readonly BankPercents _bankPercents;
    public Bank(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw BankException.BankNameIsNullException();
        }

        Name = name;
        Id = Guid.NewGuid();
        _clients = new List<Client>();
        _subscribedToNotify = new List<Client>();
        _debitAccounts = new List<DebitAccount>();
        _depositAccounts = new List<DepositAccount>();
        _creditAccounts = new List<CreditAccount>();
        _bankPercents = new BankPercents();
        Money = 0;
    }

    public IReadOnlyCollection<Client> Clients => _clients;
    public string Name { get; }
    public Guid Id { get; }
    public decimal Money { get; private set; }
    public decimal CreditLimit { get; private set; }
    public decimal LimitForDoubtful { get; private set; }
    public decimal Limit { get; private set; }
    public BankPercents BankPercents => _bankPercents;
    public Client AddClient(Client client)
    {
        if (client is null)
        {
            throw ClientException.ClientIsNullException();
        }

        if (_clients.Contains(client))
        {
            throw ClientException.ClientAlreadyExistsException();
        }

        _clients.Add(client);
        return client;
    }

    public void RemoveClient(Client client)
    {
        if (client is null)
        {
            throw ClientException.ClientIsNullException();
        }

        if (!_clients.Contains(client))
        {
            throw ClientException.ClientNotExistsException();
        }

        _clients.Remove(client);
    }

    public DebitAccount CreateDebitAccount(Client client, decimal startMoney)
    {
        if (client is null)
        {
            throw ClientException.ClientIsNullException();
        }

        if (startMoney <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        DebitAccount debitAccount = new DebitAccount(this, client, startMoney);
        AddClient(client);
        client.AddBankAccount(debitAccount);
        _debitAccounts.Add(debitAccount);
        return debitAccount;
    }

    public DepositAccount CreateDepositAccount(Client client, decimal startMoney)
    {
        if (client is null)
        {
            throw ClientException.ClientIsNullException();
        }

        if (startMoney <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        DepositAccount depositAccount = new DepositAccount(this, client, startMoney);
        AddClient(client);
        client.AddBankAccount(depositAccount);
        _depositAccounts.Add(depositAccount);
        return depositAccount;
    }

    public CreditAccount CreateCreditAccount(Client client, decimal startMoney)
    {
        if (client is null)
        {
            throw ClientException.ClientIsNullException();
        }

        if (startMoney <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        CreditAccount creditAccount = new CreditAccount(this, client, startMoney);
        AddClient(client);
        client.AddBankAccount(creditAccount);
        _creditAccounts.Add(creditAccount);
        return creditAccount;
    }

    public void CancelTransaction(Transaction transaction)
    {
        if (transaction is null)
        {
            throw TransactionException.TransactionIsNullException();
        }

        if (transaction.IsCancelled)
        {
            throw TransactionException.TransactionIsAlreadyCancelledException();
        }

        if (transaction.Sender != transaction.Recipient)
        {
            var senderBankAccount = _clients.SelectMany(c => c.BankAccounts)
                .SingleOrDefault(b => b.Id == transaction.Sender);
            var recipientBankAccount = _clients.SelectMany(c => c.BankAccounts)
                .SingleOrDefault(b => b.Id == transaction.Recipient);
            senderBankAccount?.Replenish(transaction.Money + transaction.Commission);
            recipientBankAccount?.Withdraw(transaction.Money + transaction.Commission);
            transaction.ChangeIsCancelledFlag();
            var recipientTransaction = recipientBankAccount?.Transactions.SingleOrDefault(
                t => t.TransactionTime == transaction.TransactionTime);
            recipientTransaction?.ChangeIsCancelledFlag();
        }
        else
        {
            var senderBankAccount = _clients.SelectMany(c => c.BankAccounts)
                .SingleOrDefault(b => b.Id == transaction.Sender);
            if (transaction.TransactionType == "replenish")
            {
                senderBankAccount?.Withdraw(transaction.Money + transaction.Commission);
            }
            else
            {
                senderBankAccount?.Replenish(transaction.Money + transaction.Commission);
            }

            transaction.ChangeIsCancelledFlag();
        }
    }

    public void IncrementMoney(decimal money)
    {
        if (money <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        Money += money;
    }

    public void DecrementMoney(decimal money)
    {
        if (money <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        if (Money < money)
        {
            throw TransactionException.NoEnoughMoneyException();
        }

        Money -= money;
    }

    public void SetLimit(decimal newLimit)
    {
        if (newLimit <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        Limit = newLimit;
        NotifySubscribers(_subscribedToNotify, "Transfer limit was updated!");
    }

    public void SetLimitForDoubtful(decimal newLimit)
    {
        if (newLimit <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        LimitForDoubtful = newLimit;
    }

    public void SetCreditLimit(decimal newLimit)
    {
        if (newLimit <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        CreditLimit = newLimit;
        var subscribed = _creditAccounts.Where(creditAccount => creditAccount.Client.SubscribedToNotifications)
            .Select(creditAccount => creditAccount.Client);
        NotifySubscribers(subscribed, "Credit limit was updated!");
    }

    public void SetCommission(decimal newCommission)
    {
        if (newCommission <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        _bankPercents.SetCommission(newCommission);
        var subscribed = _creditAccounts.Where(creditAccount => creditAccount.Client.SubscribedToNotifications)
            .Select(creditAccount => creditAccount.Client);
        NotifySubscribers(subscribed, "Commission was updated!");
    }

    public void SetDebitPercent(decimal newPercent)
    {
        if (newPercent <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        _bankPercents.SetDebitPercent(newPercent);
        var subscribed = _debitAccounts.Where(debitAccount => debitAccount.Client is not null && debitAccount.Client.SubscribedToNotifications)
            .Select(debitAccount => debitAccount.Client);
        NotifySubscribers(subscribed, "Debit percent was updated!");
    }

    public void SetDepositPercent(decimal startDepositMoney, decimal endDepositMoney, decimal newPercent)
    {
        if (newPercent <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        var percentForDepositRange =
            _bankPercents.DepositPercents.SingleOrDefault(p => p.Key.Key == startDepositMoney && p.Key.Value == endDepositMoney);
        var newRange = new KeyValuePair<decimal, decimal>(startDepositMoney, endDepositMoney);
        if (percentForDepositRange.Key.Key == 0 && percentForDepositRange.Key.Value == 0 && percentForDepositRange.Value == 0)
        {
            _bankPercents.AddDepositPercent(newRange, newPercent);
        }
        else
        {
            var depositRange = percentForDepositRange.Key;
            _bankPercents.RemoveDepositPercent(depositRange);
            _bankPercents.AddDepositPercent(newRange, newPercent);
        }

        var subscribed = _depositAccounts.Where(depositAccount => depositAccount.Client is not null && depositAccount.Client.SubscribedToNotifications)
            .Select(depositAccount => depositAccount.Client);
        NotifySubscribers(subscribed, "Deposit percent was updated!");
    }

    public Client AddSubscribedClient(Client client)
    {
        if (client is null)
        {
            throw ClientException.ClientIsNullException();
        }

        if (!_subscribedToNotify.Contains(client))
        {
            _subscribedToNotify.Add(client);
        }

        return client;
    }

    public void NotifySubscribers(IEnumerable<Client?> clients, string message)
    {
        _clients.ForEach(client => client.Notificator?.Notify(message));
    }

    public void AccruePercentsToAccounts()
    {
        _debitAccounts.ForEach(debitAccount => debitAccount.AccruePercents());
        _depositAccounts.ForEach(depositAccount => depositAccount.AccruePercents());
    }

    public void AddPercentsToAccountsBalance()
    {
        _debitAccounts.ForEach(debitAccount => debitAccount.AddPercentsToBalance());
        _depositAccounts.ForEach(depositAccount => depositAccount.AddPercentsToBalance());
    }

    public void Notify()
    {
        AddPercentsToAccountsBalance();
    }
}
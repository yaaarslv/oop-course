using Banks.Models;

namespace Banks.Entities;

public interface IBankAccount
{
    Guid Id { get; }
    Bank Bank { get; }
    decimal StartMoney { get; }
    decimal Money { get; }
    bool IsDoubtful { get; }
    IReadOnlyCollection<Transaction> Transactions { get; }
    void Replenish(decimal money);
    void Withdraw(decimal money);
    void Transfer(Guid recipient, decimal money);
    void ChangeDoubtToConfirmed();
    string PrintAccountData();
}
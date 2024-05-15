using Banks.Entities;

namespace Banks.Commands;

public class PrintBalanceCommand : ICommand
{
    private IBankAccount? _bankAccount;

    public PrintBalanceCommand(IBankAccount? bankAccount)
    {
        _bankAccount = bankAccount;
    }

    public void Execute()
    {
        Console.WriteLine(_bankAccount?.PrintAccountData());
    }
}
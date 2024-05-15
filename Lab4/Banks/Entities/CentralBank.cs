using Banks.Tools;

namespace Banks.Entities;

public class CentralBank
{
    private const int MonthsInYear = 12;
    private static CentralBank? _instance;
    private readonly List<Bank> _banks;

    private CentralBank()
    {
        _banks = new List<Bank>();
    }

    public IReadOnlyCollection<Bank> Banks => _banks;

    public static CentralBank GetInstance()
    {
        if (_instance is null)
        {
            _instance = new CentralBank();
        }

        return _instance;
    }

    public Bank CreateBank(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw BankException.BankNameIsNullException();
        }

        if (CheckBankExistence(name))
        {
            throw BankException.BankAlreadyCreatedException();
        }

        Bank bank = new Bank(name);
        _banks.Add(bank);
        return bank;
    }

    public void TransferMoney(Bank senderBank, Bank recipientBank, decimal money)
    {
        if (senderBank is null)
        {
            throw BankException.BankIsNullException();
        }

        if (recipientBank is null)
        {
            throw BankException.BankIsNullException();
        }

        if (money <= 0)
        {
            throw TransactionException.InvalidMoneyException();
        }

        senderBank.DecrementMoney(money);
        recipientBank.IncrementMoney(money);
    }

    public void NotifyBanks()
    {
        _banks.ForEach(bank => bank.Notify());
    }

    public void TimeAcceleration(DateOnly dateToAccelerate)
    {
        int days = dateToAccelerate.Day;
        int months = dateToAccelerate.Month;
        int years = dateToAccelerate.Year;
        for (int i = 0; i < months + (years * MonthsInYear); i++)
        {
            for (int j = 0; j < days; j++)
            {
                AccruePercentsInAllBanks();
            }

            AddPercentsInAllBanks();
        }
    }

    private void AccruePercentsInAllBanks()
    {
        _banks.ForEach(bank => bank.AccruePercentsToAccounts());
    }

    private void AddPercentsInAllBanks()
    {
        _banks.ForEach(bank => bank.AddPercentsToAccountsBalance());
    }

    private bool CheckBankExistence(string name)
    {
        return _banks.Any(b => b.Name == name);
    }
}
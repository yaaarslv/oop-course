using Banks.Tools;

namespace Banks.Models;

public class BankPercents
{
    private readonly Dictionary<KeyValuePair<decimal, decimal>, decimal> _depositPercents;

    public BankPercents()
    {
        _depositPercents = new Dictionary<KeyValuePair<decimal, decimal>, decimal>();
    }

    public decimal Commission { get; private set; }
    public decimal DebitPercent { get; private set; }
    public IReadOnlyDictionary<KeyValuePair<decimal, decimal>, decimal> DepositPercents => _depositPercents;

    public void SetCommission(decimal commission)
    {
        Commission = commission;
    }

    public void SetDebitPercent(decimal debitPercent)
    {
        DebitPercent = debitPercent;
    }

    public void AddDepositPercent(KeyValuePair<decimal, decimal> newRange, decimal newPercent)
    {
        if (CheckPercentExistence(newRange))
        {
            throw BankAccountException.PercentAlreadyContains();
        }

        _depositPercents.Add(newRange, newPercent);
    }

    public void RemoveDepositPercent(KeyValuePair<decimal, decimal> depositRange)
    {
        if (!CheckPercentExistence(depositRange))
        {
            throw BankAccountException.PercentNotExist();
        }

        _depositPercents.Remove(depositRange);
    }

    private bool CheckPercentExistence(KeyValuePair<decimal, decimal> depositRange)
    {
        return _depositPercents.ContainsKey(depositRange);
    }
}
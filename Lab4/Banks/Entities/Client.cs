using Banks.Models;
using Banks.NotifyMethods;
using Banks.Tools;

namespace Banks.Entities;

public class Client
{
    private const int FullNameComponentsCount = 3;
    private readonly List<IBankAccount> _bankAccounts;
    public Client(string? name, Address? address, Passport? passport, PhoneNumber? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw ClientException.ClientNameIsNullException();
        }

        var fullname = name.Split(" ");
        if (fullname.Length < FullNameComponentsCount)
        {
            throw ClientException.NameIsNotFullException();
        }

        FullName = name;
        FirstName = name.Split(' ')[0];
        MiddleName = name.Split(' ')[1];
        LastName = name.Split(' ')[2];
        _bankAccounts = new List<IBankAccount>();
        Address = address;
        Passport = passport;
        PhoneNumber = phoneNumber;
    }

    public string? FullName { get; }
    public string FirstName { get; }
    public string MiddleName { get; }
    public string LastName { get; }
    public IReadOnlyCollection<IBankAccount> BankAccounts => _bankAccounts;
    public Address? Address { get; private set; }
    public Passport? Passport { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public INotificator? Notificator { get; private set; }
    public bool SubscribedToNotifications { get; private set; }

    public void UpdateAddress(Address? address)
    {
        if (address is null)
        {
            throw AddressException.AddressIsNullException();
        }

        Address = address;
        if (CheckClientDataFullness())
        {
            _bankAccounts.ForEach(bankAccount => bankAccount.ChangeDoubtToConfirmed());
        }
    }

    public void UpdatePassportData(Passport? passport)
    {
        if (passport is null)
        {
            throw PassportException.PassportDataIsNullException();
        }

        Passport = passport;
        if (CheckClientDataFullness())
        {
            _bankAccounts.ForEach(bankAccount => bankAccount.ChangeDoubtToConfirmed());
        }
    }

    public void UpdatePhoneNumber(PhoneNumber? phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public bool CheckClientDataFullness()
    {
        return !(Address is null || Passport is null);
    }

    public IBankAccount AddBankAccount(IBankAccount bankAccount)
    {
        if (bankAccount is null)
        {
            throw BankAccountException.BankAccountIsNullException();
        }

        if (_bankAccounts.Contains(bankAccount))
        {
            throw BankAccountException.BankAccountAlreadyExistsException();
        }

        _bankAccounts.Add(bankAccount);
        return bankAccount;
    }

    public void RemoveBankAccount(IBankAccount bankAccount)
    {
        if (bankAccount is null)
        {
            throw BankAccountException.BankAccountIsNullException();
        }

        if (!_bankAccounts.Contains(bankAccount))
        {
            throw BankAccountException.BankAccountNotExistsException();
        }

        _bankAccounts.Remove(bankAccount);
    }

    public void SubscribeToNotifications(INotificator notificator)
    {
        Notificator = notificator;
        SubscribedToNotifications = true;
        _bankAccounts.ForEach(bankAccount => bankAccount.Bank.AddSubscribedClient(this));
    }
}
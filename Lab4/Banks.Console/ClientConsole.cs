using Banks.Commands;
using Banks.Entities;
using Banks.Models;
using Banks.NotifyMethods;
using Banks.Tools;

namespace Banks.Console;

public class ClientConsole
{
    private const int PassportSeriesLength = 4;
    private const int PassportNumberLength = 6;
    private static Client? _client;
    private ICommand? _command;
    private CentralBank _centralBank;

    public ClientConsole()
    {
        _centralBank = CentralBank.GetInstance();
        _centralBank.CreateBank("Sberbank");
        _centralBank.CreateBank("VTB");
        _client = null;
    }

    public static void Main()
    {
        var console = new ClientConsole();

        System.Console.WriteLine("Enter \"q\" to close console");
        _client = console.CreateClient();

        while (true)
        {
            System.Console.WriteLine("\nEnter \"/All banks\" to see all available banks");
            System.Console.WriteLine("Enter \"/My information\" to see information about you");
            System.Console.WriteLine("Enter \"/Update my information\" to update information about you");
            System.Console.WriteLine("Enter \"/Open bank account\" to open bank account");
            System.Console.WriteLine("Enter \"/Account balance\" to see bank account balance");
            System.Console.WriteLine("Enter \"/Replenish\" to replenish bank account");
            System.Console.WriteLine("Enter \"/Withdraw\" to withdraw bank account");
            System.Console.WriteLine("Enter \"/Transfer\" to transfer money to another bank account");
            System.Console.WriteLine("Enter \"/Subscribe to notifications\" to subscribe to notifications about updates");
            System.Console.WriteLine("Enter \"/q\" to close console");
            console.Parser(System.Console.ReadLine());
            System.Console.WriteLine();
        }
    }

    private void SetCommand(ICommand command)
    {
        _command = command;
    }

    private void Parser(string? command)
    {
        switch (command)
        {
            case "/All banks":
                PrintAllBanks();
                System.Console.WriteLine();
                break;
            case "/Open bank account":
                CreateBankAccount();
                System.Console.WriteLine();
                break;
            case "/My information":
                PrintClientData();
                System.Console.WriteLine();
                break;
            case "/Update my information":
                UpdateClientData();
                System.Console.WriteLine();
                break;
            case "/Subscribe to notifications":
                _client?.SubscribeToNotifications(new SimpleNotification());
                System.Console.WriteLine();
                break;
            case "/Account balance":
                PrintAccountBalance();
                break;
            case "/Replenish":
                Replenish();
                System.Console.WriteLine();
                break;
            case "/Withdraw":
                Withdraw();
                System.Console.WriteLine();
                break;
            case "/Transfer":
                Transfer();
                System.Console.WriteLine();
                break;
            case "/q":
                Environment.Exit(0);
                break;
        }
    }

    private void PrintAccountBalance()
    {
        System.Console.WriteLine("\nEnter id of bank account:");
        string? stringId = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(stringId))
        {
            System.Console.WriteLine("\nYou entered invalid id!");
        }

        if (stringId is not null)
        {
            Guid guid = Guid.Parse(stringId);
            var bankAccount = _client?.BankAccounts.SingleOrDefault(b => b.Id == guid);
            SetCommand(new PrintBalanceCommand(bankAccount));
            _command?.Execute();
        }
    }

    private void Transfer()
    {
        System.Console.WriteLine("\nEnter money to transfer:");
        string? stringMoney = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(stringMoney))
        {
            System.Console.WriteLine("\nYou entered invalid money!");
        }

        decimal money = decimal.Parse(stringMoney ?? string.Empty);
        if (money < 0)
        {
            System.Console.WriteLine("\nYou entered invalid money!");
        }

        System.Console.WriteLine("\nEnter id of bank account from you want to transfer");
        string? senderId = System.Console.ReadLine();
        System.Console.WriteLine("\nEnter id of bank account where you want to transfer");
        string? recipientId = System.Console.ReadLine();
        if (senderId is not null && recipientId is not null)
        {
            Guid senderGuid = Guid.Parse(senderId);
            Guid recipientGuid = Guid.Parse(recipientId);
            var bankAccount = _client?.BankAccounts.SingleOrDefault(b => b.Id == senderGuid);
            bankAccount?.Transfer(recipientGuid, money);
        }
    }

    private void Withdraw()
    {
        System.Console.WriteLine("Enter money to withdraw:");
        string? stringMoney = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(stringMoney))
        {
            System.Console.WriteLine("\nYou entered invalid money!");
        }

        decimal money = decimal.Parse(stringMoney ?? string.Empty);
        if (money < 0)
        {
            System.Console.WriteLine("\nYou entered invalid money!");
        }

        System.Console.WriteLine("\nEnter id of bank account from you want to withdraw");
        string? id = System.Console.ReadLine();
        if (id is not null)
        {
            Guid guid = Guid.Parse(id);
            var bankAccount = _client?.BankAccounts.SingleOrDefault(b => b.Id == guid);
            bankAccount?.Withdraw(money);
        }
    }

    private void Replenish()
    {
        System.Console.WriteLine("\nEnter money to replenish:");
        string? stringMoney = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(stringMoney))
        {
            System.Console.WriteLine("\nYou entered invalid money!");
        }

        decimal money = decimal.Parse(stringMoney ?? string.Empty);
        if (money < 0)
        {
            System.Console.WriteLine("\nYou entered invalid money!");
        }

        System.Console.WriteLine("\nEnter id of bank account you want to replenish");
        string? id = System.Console.ReadLine();
        if (id is not null)
        {
            Guid guid = Guid.Parse(id);
            var bankAccount = _client?.BankAccounts.SingleOrDefault(b => b.Id == guid);
            bankAccount?.Replenish(money);
        }
    }

    private void UpdateClientData()
    {
        System.Console.WriteLine("What information do you want to update?");
        string? answer = System.Console.ReadLine();
        switch (answer)
        {
            case "Passport":
                System.Console.WriteLine("\nEnter passport series:");
                string? series = System.Console.ReadLine();
                System.Console.WriteLine("\nEnter passport number:");
                string? number = System.Console.ReadLine();
                System.Console.WriteLine("\nEnter information about by whom the passport was issued:");
                string? issuedBy = System.Console.ReadLine();
                System.Console.WriteLine("\nEnter information about when the passport is issued:");
                string? issueDate = System.Console.ReadLine();
                var passport = new Passport($"{series}, {number}, {issuedBy}, {issueDate}");
                _client?.UpdatePassportData(passport);
                break;
            case "Address":
                System.Console.WriteLine("\nEnter country:");
                string? country = System.Console.ReadLine();
                System.Console.WriteLine("\nEnter region:");
                string? region = System.Console.ReadLine();
                System.Console.WriteLine("\nEnter city:");
                string? city = System.Console.ReadLine();
                System.Console.WriteLine("\nEnter street:");
                string? street = System.Console.ReadLine();
                System.Console.WriteLine("\nEnter house:");
                string? house = System.Console.ReadLine();
                System.Console.WriteLine("\nEnter flat:");
                string? flat = System.Console.ReadLine();
                var address = new Address($"{country}, {region}, {city}, {street}, {house}, {flat}");
                _client?.UpdateAddress(address);
                break;
            case "Phonenumber":
                System.Console.WriteLine("\nEnter phoneNumber:");
                string? phone = System.Console.ReadLine();
                var phoneNumber = new PhoneNumber(phone);
                _client?.UpdatePhoneNumber(phoneNumber);
                break;
        }
    }

    private void PrintClientData()
    {
        if (_client is not null)
        {
            System.Console.WriteLine($"FIO: {_client.FullName}");
            System.Console.WriteLine($"Address: {_client.Address?.StringAddress()}");
            System.Console.WriteLine($"Passport: {_client.Passport?.StringPassportData()}");
            System.Console.WriteLine($"Phonenumber: {_client.PhoneNumber?.Phone}");
            var isSubscribed = _client.SubscribedToNotifications ? "True" : "False";
            System.Console.WriteLine($"Subscribed to updates: {isSubscribed}");
            System.Console.WriteLine($"Count of bank accounts: {_client.BankAccounts.Count}");
        }
    }

    private IBankAccount? CreateBankAccount()
    {
        System.Console.WriteLine("\nEnter name of bank:");
        string? bankName = System.Console.ReadLine();
        var bank = _centralBank.Banks.SingleOrDefault(b => b.Name == bankName);
        if (bank is null)
        {
            System.Console.WriteLine("\nBank with this name doesn't exist!");
        }

        System.Console.WriteLine("\nEnter type of bank account you need: (debit/deposit/credit)");
        string? bankAccountType = System.Console.ReadLine();

        IBankAccount? bankAccount = null;
        string? stringStartMoney;
        decimal startMoney;
        switch (bankAccountType)
        {
            case "debit":
                System.Console.WriteLine("\nEnter start balance:");
                stringStartMoney = System.Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stringStartMoney))
                {
                    System.Console.WriteLine("\nYou entered invalid money!");
                }

                startMoney = decimal.Parse(stringStartMoney ?? string.Empty);
                if (startMoney < 0)
                {
                    System.Console.WriteLine("\nYou entered invalid money!");
                }

                if (_client is not null) bankAccount = bank?.CreateDebitAccount(_client, startMoney);
                break;
            case "deposit":
                System.Console.WriteLine("\nEnter start balance:");
                stringStartMoney = System.Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stringStartMoney))
                {
                    System.Console.WriteLine("\nYou entered invalid money!");
                }

                startMoney = decimal.Parse(stringStartMoney ?? string.Empty);
                if (startMoney < 0)
                {
                    System.Console.WriteLine("\nYou entered invalid money!");
                }

                if (_client is not null) bankAccount = bank?.CreateDepositAccount(_client, startMoney);
                break;
            case "credit":
                System.Console.WriteLine("\nEnter credit size:");
                stringStartMoney = System.Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stringStartMoney))
                {
                    System.Console.WriteLine("\nYou entered invalid money!");
                }

                startMoney = decimal.Parse(stringStartMoney ?? string.Empty);
                if (startMoney < 0)
                {
                    System.Console.WriteLine("\nYou entered invalid money!");
                }

                if (_client is not null) bankAccount = bank?.CreateCreditAccount(_client, startMoney);
                break;
        }

        System.Console.WriteLine($"Id of this bank account is {bankAccount?.Id}");
        return bankAccount;
    }

    private void PrintAllBanks()
    {
        int count = 1;
        foreach (var bank in _centralBank.Banks)
        {
            System.Console.WriteLine($"{count}) {bank.Name}");
            count++;
        }
    }

    private Client CreateClient()
    {
        Passport? passport = null;
        Address? address = null;
        PhoneNumber? phoneNumber = null;

        System.Console.WriteLine("\nEnter your FirstName, MiddleName and LastName:");
        string? fio = System.Console.ReadLine();
        var clientBuilder = new ClientBuilder(fio);

        System.Console.WriteLine("\nDo you want to enter passport data? (yes/no)");
        string? passportAnswer = System.Console.ReadLine();
        switch (passportAnswer)
        {
            case "yes":
                string? series, number, issuedBy, issueDate;
                while (true)
                {
                    System.Console.WriteLine("\nEnter passport series:");
                    series = System.Console.ReadLine();
                    if (series?.Length == PassportSeriesLength && int.TryParse(series, out int ser))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid passport series!");
                }

                while (true)
                {
                    System.Console.WriteLine("\nEnter passport number:");
                    number = System.Console.ReadLine();
                    if (number?.Length == PassportNumberLength && int.TryParse(number, out int num))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid passport number!");
                }

                while (true)
                {
                    System.Console.WriteLine("\nEnter information about by whom the passport was issued:");
                    issuedBy = System.Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(issuedBy))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid information about by whom passport was issued!");
                }

                while (true)
                {
                    System.Console.WriteLine("\nEnter information about when the passport is issued:");
                    issueDate = System.Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(issueDate) && DateOnly.TryParse(issueDate, out DateOnly date))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid information about when passport was issued!");
                }

                passport = new Passport($"{series}, {number}, {issuedBy}, {issueDate}");
                clientBuilder.AddPassportData(passport);
                break;

            case "no":
                System.Console.WriteLine("\nYou'll regret it...");
                break;

            case "q":
                Environment.Exit(0);
                break;
        }

        System.Console.WriteLine("\nDo you want to enter address data? (yes/no)");
        string? addressAnswer = System.Console.ReadLine();
        switch (addressAnswer)
        {
            case "yes":
                string? country, region, city, street, house, flat;
                while (true)
                {
                    System.Console.WriteLine("\nEnter country:");
                    country = System.Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(country))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid country!");
                }

                while (true)
                {
                    System.Console.WriteLine("\nEnter region:");
                    region = System.Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(region))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid region!");
                }

                while (true)
                {
                    System.Console.WriteLine("\nEnter city:");
                    city = System.Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(city))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid city!");
                }

                while (true)
                {
                    System.Console.WriteLine("\nEnter street:");
                    street = System.Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(street))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid street!");
                }

                while (true)
                {
                    System.Console.WriteLine("\nEnter house:");
                    house = System.Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(house) && int.TryParse(house, out int h))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid house!");
                }

                while (true)
                {
                    System.Console.WriteLine("\nEnter flat:");
                    flat = System.Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(flat) && int.TryParse(flat, out int f))
                    {
                        break;
                    }

                    System.Console.WriteLine("You entered invalid flat!");
                }

                address = new Address($"{country}, {region}, {city}, {street}, {house}, {flat}");
                clientBuilder.AddAddress(address);
                break;

            case "no":
                System.Console.WriteLine("\nYou'll regret it...");
                break;

            case "q":
                Environment.Exit(0);
                break;
        }

        System.Console.WriteLine("\nDo you want to enter phoneNumber? (yes/no)");
        string? phoneAnswer = System.Console.ReadLine();
        switch (phoneAnswer)
        {
            case "yes":
                System.Console.WriteLine("\nEnter phoneNumber:");
                string? phone = System.Console.ReadLine();
                phoneNumber = new PhoneNumber(phone);
                clientBuilder.AddPhoneNumber(phoneNumber);
                break;

            case "no":
                System.Console.WriteLine("\nYou won't regret it :)");
                break;

            case "q":
                Environment.Exit(0);
                break;
        }

        var client = clientBuilder.CreateClient();
        return client;
    }
}
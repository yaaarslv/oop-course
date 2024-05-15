using Banks.Entities;
using Banks.Models;
using Banks.NotifyMethods;
using Xunit;

namespace Banks.Test;

public class BanksTest
{
    private CentralBank _centralBank = CentralBank.GetInstance();

    [Fact]
    public void CreateBankAndSetPercents()
    {
        Bank bank = _centralBank.CreateBank("Sberbank");
        bank.SetDebitPercent(5);
        Assert.Contains(bank, _centralBank.Banks);
        Assert.True(bank.BankPercents.DebitPercent == 5);
    }

    [Fact]
    public void CreateClientAndAccount()
    {
        Bank bank = _centralBank.CreateBank("DrLiveseybank");
        var clientBuilder = new ClientBuilder("Ливси Доктор Ахахахахахахахович");
        Address address = new Address("Добавьте, в беседу, бота, @DrLevseyBot, 1, 2");
        clientBuilder.AddAddress(address);
        Passport passport = new Passport("1234, 567890, МВД Казахстана, 2003.05.12");
        clientBuilder.AddPassportData(passport);
        PhoneNumber phoneNumber = new PhoneNumber("+79123456789");
        Client client = clientBuilder.CreateClient();
        bank.CreateDepositAccount(client, 10000);
        Assert.Contains(client, bank.Clients);
        Assert.True(client.BankAccounts.First().StartMoney == 10000);
    }

    [Fact]
    public void CheckCommissionIsAccrued()
    {
        Bank bank = _centralBank.CreateBank("Bebrabank");
        bank.SetCommission(50);
        bank.SetLimitForDoubtful(15000);
        var clientBuilder = new ClientBuilder("Ливси Доктор Ахахахахахахахович");
        Client client = clientBuilder.CreateClient();
        CreditAccount creditAccount = bank.CreateCreditAccount(client, 10000);
        creditAccount.Withdraw(200);
        Assert.True(creditAccount.Transactions.First().Commission == 50);
    }

    [Fact]
    public void CanselTransaction()
    {
        Bank bank = _centralBank.CreateBank("Sber");
        var clientBuilder = new ClientBuilder("Ливси Доктор Ахахахахахахахович");
        Client client = clientBuilder.CreateClient();
        DebitAccount debitAccount = bank.CreateDebitAccount(client, 10000);
        debitAccount.Withdraw(9999);
        bank.CancelTransaction(debitAccount.Transactions.First());
        Assert.True(debitAccount.Transactions.First().IsCancelled);
        Assert.True(debitAccount.Money == 10000);
    }

    [Fact]
    public void SubscribeToNotificationsAndUpdateLimit()
    {
        Bank bank = _centralBank.CreateBank("VTB");
        var clientBuilder = new ClientBuilder("Ливси Доктор Ахахахахахахахович");
        Client client = clientBuilder.CreateClient();
        DebitAccount debitAccount = bank.CreateDebitAccount(client, 10000);
        client.SubscribeToNotifications(new SimpleNotification());
        bank.SetLimit(30000);
        Assert.True(client.SubscribedToNotifications);
        Assert.True(client.Notificator?.IsNotified);
    }
}
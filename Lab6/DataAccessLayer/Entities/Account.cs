using System.Security.Cryptography;
using System.Text;
using DataAccessLayer.Tools;

namespace DataAccessLayer.Entities;

public class Account
{
    private string _password;
    public Account(Worker worker, string login, string password)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        if (string.IsNullOrWhiteSpace(login))
        {
            throw AccountException.LoginIsNullException();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw AccountException.PasswordIsNullException();
        }

        Worker = worker;
        Login = login;
        _password = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password)).ToString();
    }

    public Worker Worker { get; }
    public string Login { get; }
}
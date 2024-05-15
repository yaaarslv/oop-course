using BusinessLayer.Services;
using DataAccessLayer.DataBases;
using DataAccessLayer.Tools;

namespace BusinessLayer.Authentication;

public static class Authentication
{
    public static MessageService Authenticate(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw AccountException.LoginIsNullException();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw AccountException.PasswordIsNullException();
        }

        if (!WorkersDataBase.GetInstance().CheckAccountExistence(login, password))
        {
            throw AccountException.AccountNotExistsException();
        }

        return new MessageService();
    }
}
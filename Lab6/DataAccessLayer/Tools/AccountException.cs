using DataAccessLayer.Entities;

namespace DataAccessLayer.Tools;

public class AccountException : Exception
{
    private AccountException(string message)
        : base(message) { }

    public static AccountException AccountIsNullException()
    {
        return new AccountException("Account is null!");
    }

    public static AccountException LoginIsNullException()
    {
        return new AccountException("Login text is null!");
    }

    public static AccountException PasswordIsNullException()
    {
        return new AccountException("Password text is null!");
    }

    public static AccountException AccountNotExistsException()
    {
        return new AccountException("Account with this login and password doesn't exist!");
    }
}

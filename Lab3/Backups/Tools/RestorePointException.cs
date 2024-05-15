namespace Backups.Tools;

public class RestorePointException : Exception
{
    private RestorePointException(string message)
        : base(message) { }

    public static RestorePointException RestorePointIsNullException()
    {
        return new RestorePointException("Restore point is null!");
    }

    public static RestorePointException RestorePointNotContainException()
    {
        return new RestorePointException("Restore point doesn't exist in the list of restore points!");
    }

    public static RestorePointException RestorePointAlreadyExists()
    {
        return new RestorePointException("Restore point already exists in the list of restore points!");
    }
}
namespace Backups.Tools;

public class BackupTaskException : Exception
{
    private BackupTaskException(string message)
        : base(message) { }

    public static BackupTaskException BackupTaskIsNullException()
    {
        return new BackupTaskException("Backup task is null!");
    }
}
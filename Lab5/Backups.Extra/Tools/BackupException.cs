namespace Backups.Extra.Tools;

public class BackupException : Exception
{
    private BackupException(string message)
        : base(message) { }

    public static BackupException BackupIsNullException()
    {
        return new BackupException("Backup is null!");
    }

    public static BackupException PathIsNull()
    {
        return new BackupException("Path is null!");
    }
}
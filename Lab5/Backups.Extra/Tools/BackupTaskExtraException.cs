namespace Backups.Extra.Tools;

public class BackupTaskExtraException : Exception
{
    private BackupTaskExtraException(string message)
        : base(message) { }

    public static BackupTaskExtraException BackupTaskExtraIsNull()
    {
        return new BackupTaskExtraException("Backup task is null!");
    }
}
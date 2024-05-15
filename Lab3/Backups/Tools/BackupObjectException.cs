using System.Reflection.Metadata.Ecma335;
using Backups.Entities;

namespace Backups.Tools;

public class BackupObjectException : Exception
{
    private BackupObjectException(string message)
        : base(message) { }

    public static BackupObjectException PathIsNullException()
    {
        return new BackupObjectException("Path is null!");
    }

    public static BackupObjectException NameIsNullException()
    {
        return new BackupObjectException("Name is null!");
    }

    public static BackupObjectException BackupObjectIsNullException()
    {
        return new BackupObjectException("Backup object is null!");
    }

    public static BackupObjectException BackupObjectNotContainException()
    {
        return new BackupObjectException("Backup object doesn't exist in the list of tracked objects!");
    }

    public static BackupObjectException BackupObjectAlreadyExists()
    {
        return new BackupObjectException("Backup object already exists in the list of tracked objects!");
    }
}
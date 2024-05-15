using Backups.Entities;

namespace Backups.Tools;

public class StorageException : Exception
{
    private StorageException(string message)
        : base(message) { }

    public static StorageException ArchiveIsNullException()
    {
        return new StorageException("Archive is null!");
    }

    public static StorageException StorageNameIsNullException()
    {
        return new StorageException("Storage name is null!");
    }

    public static StorageException StorageIsNullException()
    {
        return new StorageException("Storage is null!");
    }
}
using Backups.Tools;
using Ionic.Zip;

namespace Backups.Entities;

public class Storage
{
    public Storage(ZipFile archive, string storageName)
    {
        if (archive is null)
        {
            throw StorageException.ArchiveIsNullException();
        }

        if (string.IsNullOrWhiteSpace(storageName))
        {
            throw StorageException.StorageNameIsNullException();
        }

        Archive = archive;
        StorageName = storageName;
    }

    public ZipFile Archive { get; }
    public string StorageName { get; }
}
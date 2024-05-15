using Backups.Tools;

namespace Backups.Entities;

public class RestorePoint
{
    private List<BackupObject> _backupObjects;
    private List<Storage> _storages;
    public RestorePoint(List<BackupObject> backupObjects, int restoreNumber)
    {
        _backupObjects = backupObjects;
        CreateTime = DateTime.Now;
        string restoreNum = restoreNumber == 0 ? string.Empty : "-" + restoreNumber.ToString();
        Name = $"Restore_point-{CreateTime.ToShortDateString()}{restoreNum}";
        _storages = new List<Storage>();
    }

    public DateTime CreateTime { get; }
    public string Name { get; }
    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public IReadOnlyCollection<Storage> Storages => _storages;

    public Storage AddStorage(Storage storage)
    {
        if (storage is null)
        {
            throw StorageException.StorageIsNullException();
        }

        _storages.Add(storage);
        return storage;
    }
}
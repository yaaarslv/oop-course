using Backups.Entities;
using Backups.Tools;
using Ionic.Zip;

namespace Backups.Algorithms;

public class SingleStorage : IAlgorithm
{
    private List<Storage> _storages;

    public SingleStorage()
    {
        _storages = new List<Storage>();
    }

    public IReadOnlyCollection<Storage> CreateArchive(BackupTask backupTask)
    {
        if (backupTask is null)
        {
            throw BackupTaskException.BackupTaskIsNullException();
        }

        _storages.Clear();

        ZipFile archive = new ZipFile();
        foreach (var backupObject in backupTask.BackupObjects)
        {
            archive.AddItem(backupObject.Path);
        }

        string restoreNumber = backupTask.RestoreNumber == 0 ? string.Empty : "-" + backupTask.RestoreNumber.ToString();
        Storage storage = new Storage(archive, $"Group_archive{restoreNumber}");
        _storages.Add(storage);
        return _storages;
    }
}
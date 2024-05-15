using Backups.Entities;
using Backups.Tools;
using Ionic.Zip;

namespace Backups.Algorithms;

public class SplitStorage : IAlgorithm
{
    private List<Storage> _storages;

    public SplitStorage()
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

        foreach (var backupObject in backupTask.BackupObjects)
        {
            ZipFile archive = new ZipFile();
            archive.AddItem(backupObject.Path);
            string restoreNumber = backupTask.RestoreNumber == 0 ? string.Empty : "-" + backupTask.RestoreNumber.ToString();
            Storage storage = new Storage(archive, $"{backupObject.Name}{restoreNumber}");
            _storages.Add(storage);
        }

        return _storages;
    }
}
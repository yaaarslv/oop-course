using Backups.Algorithms;
using Backups.Tools;

namespace Backups.Entities;

public class Repository
{
    public Repository(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw RepositoryException.PathIsNullException();
        }

        Path = path;
    }

    public string Path { get; }

    public void SaveBackup(BackupTask backupTask, IAlgorithm algorithm, RestorePoint lastRestorePoint)
    {
        if (backupTask is null)
        {
            throw BackupTaskException.BackupTaskIsNullException();
        }

        if (algorithm is null)
        {
            throw AlgorithmException.AlgorithmIsNullException();
        }

        if (lastRestorePoint is null)
        {
            throw RestorePointException.RestorePointIsNullException();
        }

        var directoryInfo = Directory.CreateDirectory(@$"{Path}{System.IO.Path.DirectorySeparatorChar}{lastRestorePoint.Name}");

        IReadOnlyCollection<Storage> storages = algorithm.CreateArchive(backupTask);
        foreach (var storage in storages)
        {
            lastRestorePoint.AddStorage(storage);
        }

        Archiver.SaveArchive(storages, directoryInfo);
    }
}
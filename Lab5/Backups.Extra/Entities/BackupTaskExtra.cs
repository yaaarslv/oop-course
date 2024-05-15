using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.ClearAlgorithms;
using Backups.Extra.LoggingAlgorithms;
using Backups.Extra.RecoveryAlgorithms;
using Backups.Extra.Tools;
using Backups.Tools;

namespace Backups.Extra.Entities;

public class BackupTaskExtra : BackupTask
{
    public BackupTaskExtra(Repository repository, IAlgorithm algorithm, ILogger logger)
        : base(repository, algorithm)
    {
        if (repository is null)
        {
            throw RepositoryException.RepositoryIsNullException();
        }

        if (algorithm is null)
        {
            throw AlgorithmException.AlgorithmIsNullException();
        }

        if (logger is null)
        {
            throw LoggerException.LoggerIsNullException();
        }

        Algorithm = algorithm;
        Repository = repository;
        Logger = logger;
    }

    public IAlgorithm Algorithm { get; }
    public Repository Repository { get; }
    public ILogger Logger { get; }

    public void ClearRestorePoints(IClear clearAlgorithm)
    {
        var pointsToDelete = clearAlgorithm.Clear(RestorePoints);

        foreach (var point in pointsToDelete)
        {
            var dirInfo = new DirectoryInfo(@$"{Repository.Path}{Path.DirectorySeparatorChar}{point.Name}");
            foreach (var file in dirInfo.GetFiles())
            {
                file.Delete();
            }

            RemoveRestorePoint(point);
            Directory.Delete(@$"{Repository.Path}{Path.DirectorySeparatorChar}{point.Name}");
        }

        Logger.Log("Chosen restore points were cleared!");
    }

    public RestorePoint Merge(RestorePoint oldRestorePoint, RestorePoint newRestorePoint)
    {
        if (oldRestorePoint is null || newRestorePoint is null)
        {
            throw RestorePointException.RestorePointIsNullException();
        }

        var mergedList = newRestorePoint.BackupObjects.ToList();

        if (Algorithm.GetType() == new SingleStorage().GetType())
        {
            RemoveRestorePoint(oldRestorePoint);
        }
        else
        {
            mergedList.AddRange(oldRestorePoint.BackupObjects.Where(backupObject => !newRestorePoint.BackupObjects.Contains(backupObject)));
        }

        var mergedRestorePoint = new RestorePoint(mergedList, RestoreNumber);
        Repository.SaveBackup(this, Algorithm, mergedRestorePoint);
        foreach (var storage in oldRestorePoint.Storages)
        {
            if (!mergedRestorePoint.Storages.Contains(storage))
            {
                mergedRestorePoint.AddStorage(storage);
            }
        }

        AddRestorePoint(mergedRestorePoint);
        Logger.Log("Chosen restore points were merged!");
        return mergedRestorePoint;
    }

    public void Recovery(RestorePoint restorePoint, IRecovery recoveryAlgorithm)
    {
        recoveryAlgorithm.Recovery(restorePoint, Logger);
    }
}
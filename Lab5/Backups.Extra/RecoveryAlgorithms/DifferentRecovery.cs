using Backups.Entities;
using Backups.Extra.LoggingAlgorithms;
using Backups.Extra.Models;
using Backups.Extra.Tools;
using Backups.Tools;

namespace Backups.Extra.RecoveryAlgorithms;

public class DifferentRecovery : IRecovery
{
    public DifferentRecovery(Repository repository)
    {
        if (repository is null)
        {
            throw RepositoryException.RepositoryIsNullException();
        }

        Repository = repository;
    }

    public Repository Repository { get; }
    public void Recovery(RestorePoint restorePoint, ILogger logger)
    {
        if (restorePoint is null)
        {
            throw RestorePointException.RestorePointIsNullException();
        }

        if (logger is null)
        {
            throw LoggerException.LoggerIsNullException();
        }

        foreach (var storage in restorePoint.Storages)
        {
            ArchiverExtra.RestoreFile(storage, new DirectoryInfo(Repository.Path));
        }

        logger.Log("Chosen restore point was recovered!");
    }
}
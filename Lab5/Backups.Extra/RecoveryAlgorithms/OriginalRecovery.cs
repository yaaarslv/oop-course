using Backups.Entities;
using Backups.Extra.LoggingAlgorithms;
using Backups.Extra.Models;
using Backups.Extra.Tools;
using Backups.Tools;

namespace Backups.Extra.RecoveryAlgorithms;

public class OriginalRecovery : IRecovery
{
    public OriginalRecovery()
    { }

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
            foreach (var file in storage.Archive)
            {
                ArchiverExtra.RestoreFile(storage, new DirectoryInfo(file.FileName));
            }
        }

        logger.Log("Chosen restore point was recovered!");
    }
}
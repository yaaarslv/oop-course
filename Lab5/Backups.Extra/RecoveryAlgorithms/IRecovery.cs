using Backups.Entities;
using Backups.Extra.LoggingAlgorithms;

namespace Backups.Extra.RecoveryAlgorithms;

public interface IRecovery
{
    void Recovery(RestorePoint restorePoint, ILogger logger);
}
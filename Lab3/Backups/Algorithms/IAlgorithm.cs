using Backups.Entities;

namespace Backups.Algorithms;

public interface IAlgorithm
{
    IReadOnlyCollection<Storage> CreateArchive(BackupTask backupTask);
}
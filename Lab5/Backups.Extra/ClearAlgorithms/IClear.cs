using Backups.Entities;

namespace Backups.Extra.ClearAlgorithms;

public interface IClear
{
    IReadOnlyCollection<RestorePoint> Clear(IReadOnlyCollection<RestorePoint> restorePoints);
}
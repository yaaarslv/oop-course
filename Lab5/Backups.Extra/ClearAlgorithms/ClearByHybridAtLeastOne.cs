using Backups.Entities;
using Backups.Extra.Tools;

namespace Backups.Extra.ClearAlgorithms;

public class ClearByHybridAtLeastOne : IClear
{
    private ClearByCount _clearByCount;
    private ClearByDate _clearByDate;
    public ClearByHybridAtLeastOne(ClearByCount clearByCount, ClearByDate clearByDate)
    {
        if (clearByCount is null || clearByDate is null)
        {
            throw ClearAlgorithmException.ClearAlgorithmIsNull();
        }

        _clearByCount = clearByCount;
        _clearByDate = clearByDate;
    }

    public IReadOnlyCollection<RestorePoint> Clear(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        var deletingByCount = _clearByCount.Clear(restorePoints);
        var deletingByDate = _clearByDate.Clear(restorePoints);
        var pointsToDelete = restorePoints.Where(restorePoint => CheckRestorePointExistence(restorePoint, deletingByCount, deletingByDate)).ToList();
        return pointsToDelete;
    }

    private bool CheckRestorePointExistence(RestorePoint restorePoint, IReadOnlyCollection<RestorePoint> deletingByCount, IReadOnlyCollection<RestorePoint> deletingByDate)
    {
        return deletingByCount.Contains(restorePoint) || deletingByDate.Contains(restorePoint);
    }
}
using Backups.Entities;
using Backups.Extra.Tools;

namespace Backups.Extra.ClearAlgorithms;

public class ClearByDate : IClear
{
    public ClearByDate(DateTime dateTime)
    {
        DateTimeLimit = dateTime;
    }

    public DateTime DateTimeLimit { get; }

    public IReadOnlyCollection<RestorePoint> Clear(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        var restorePointsList = restorePoints.ToList();
        var pointsToDelete = new List<RestorePoint>();
        if (CheckRequirement(restorePoints))
        {
            pointsToDelete.AddRange(restorePoints.Where(restorePoint => restorePoint.CreateTime < DateTimeLimit));
            if (pointsToDelete.Count == restorePoints.Count)
            {
                throw ClearAlgorithmException.DeletingAllRestorePoints();
            }
        }

        return pointsToDelete;
    }

    private bool CheckRequirement(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        return restorePoints.Any(r => r.CreateTime < DateTimeLimit);
    }
}
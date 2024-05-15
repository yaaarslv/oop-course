using Backups.Entities;
using Backups.Extra.Tools;

namespace Backups.Extra.ClearAlgorithms;

public class ClearByCount : IClear
{
    public ClearByCount(int restorePointsLimit)
    {
        RestorePointsLimit = restorePointsLimit;
    }

    public int RestorePointsLimit { get; }

    public IReadOnlyCollection<RestorePoint> Clear(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        var restorePointsList = restorePoints.ToList();
        var pointsToDelete = new List<RestorePoint>();
        if (CheckRequirement(restorePoints))
        {
            for (int i = 0; i < restorePointsList.Count - RestorePointsLimit; i++)
            {
                pointsToDelete.Add(restorePointsList[i]);
            }

            if (pointsToDelete.Count == restorePoints.Count)
            {
                throw ClearAlgorithmException.DeletingAllRestorePoints();
            }
        }

        return pointsToDelete;
    }

    private bool CheckRequirement(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        return restorePoints.Count > RestorePointsLimit;
    }
}
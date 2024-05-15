using Backups.Algorithms;
using Backups.Tools;

namespace Backups.Entities;

public class BackupTask
{
    private List<BackupObject> _backupObjects;
    private List<RestorePoint> _restorePoints;
    private Repository _repository;
    private IAlgorithm _algorithm;
    public BackupTask(Repository repository, IAlgorithm algorithm)
    {
        if (repository is null)
        {
            throw RepositoryException.RepositoryIsNullException();
        }

        if (algorithm is null)
        {
            throw AlgorithmException.AlgorithmIsNullException();
        }

        _repository = repository;
        _algorithm = algorithm;
        _backupObjects = new List<BackupObject>();
        _restorePoints = new List<RestorePoint>();
    }

    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;
    public int RestoreNumber => _restorePoints.Count;

    public BackupObject AddBackupObject(BackupObject backupObject)
    {
        if (backupObject is null)
        {
            throw BackupObjectException.BackupObjectIsNullException();
        }

        if (_backupObjects.Contains(backupObject))
        {
            throw BackupObjectException.BackupObjectAlreadyExists();
        }

        _backupObjects.Add(backupObject);
        return backupObject;
    }

    public void RemoveBackupObject(BackupObject backupObject)
    {
        if (backupObject is null)
        {
            throw BackupObjectException.BackupObjectIsNullException();
        }

        if (!_backupObjects.Contains(backupObject))
        {
            throw BackupObjectException.BackupObjectNotContainException();
        }

        _backupObjects.Remove(backupObject);
    }

    public void SaveNewBackup()
    {
        RestorePoint restorePoint = new RestorePoint(_backupObjects, RestoreNumber);
        _repository.SaveBackup(this, _algorithm, restorePoint);
        _restorePoints.Add(restorePoint);
    }

    public void ChangeAlgorithm(IAlgorithm newAlgorithm)
    {
        if (newAlgorithm is null)
        {
            throw AlgorithmException.AlgorithmIsNullException();
        }

        _algorithm = newAlgorithm;
    }

    public RestorePoint AddRestorePoint(RestorePoint restorePoint)
    {
        if (restorePoint is null)
        {
            throw RestorePointException.RestorePointIsNullException();
        }

        if (_restorePoints.Contains(restorePoint))
        {
            throw RestorePointException.RestorePointAlreadyExists();
        }

        _restorePoints.Add(restorePoint);
        return restorePoint;
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        if (restorePoint is null)
        {
            throw RestorePointException.RestorePointIsNullException();
        }

        if (!_restorePoints.Contains(restorePoint))
        {
            throw RestorePointException.RestorePointNotContainException();
        }

        _restorePoints.Remove(restorePoint);
    }
}
using DataAccessLayer.Tools;

namespace DataAccessLayer.Entities;

public class Worker
{
    private readonly List<Worker> _subordinates;
    public Worker(string name, int accessLevel)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw WorkerException.WorkerNameIsNullException();
        }

        if (accessLevel < 0)
        {
            throw WorkerException.InvalidAccessLevelException();
        }

        Name = name;
        AccessLevel = accessLevel;
        _subordinates = new List<Worker>();
    }

    public string Name { get; }
    public Worker Director { get; private set; }
    public int AccessLevel { get; private set; }
    public Device Device { get; private set; }

    public Worker AddSubordinate(Worker worker)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        if (_subordinates.Contains(worker))
        {
            throw WorkerException.WorkerAlreadyExistsException();
        }

        _subordinates.Add(worker);
        return worker;
    }

    public void RemoveSubordinate(Worker worker)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        if (!_subordinates.Contains(worker))
        {
            throw WorkerException.WorkerNotExistsException();
        }

        _subordinates.Remove(worker);
    }

    public void SetAccessLevel(int newLevel)
    {
        if (newLevel < 0)
        {
            throw WorkerException.InvalidAccessLevelException();
        }

        AccessLevel = newLevel;
    }

    public void SetDirector(Worker worker)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        Director = worker;
    }

    public void SetDevice(Device newDevice)
    {
        if (newDevice is null)
        {
            throw WorkerException.DeviceIsNullException();
        }

        if (!newDevice.IsFree)
        {
            throw WorkerException.DeviceIsOccupiedException();
        }

        Device = newDevice;
        Device.ChangeToOccupied();
    }
}
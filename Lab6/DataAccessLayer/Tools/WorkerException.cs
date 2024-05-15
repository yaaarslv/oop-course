namespace DataAccessLayer.Tools;

public class WorkerException : Exception
{
    private WorkerException(string message)
        : base(message) { }

    public static WorkerException WorkerIsNullException()
    {
        return new WorkerException("Worker is null!");
    }

    public static WorkerException WorkerAlreadyExistsException()
    {
        return new WorkerException("Worker already exists in list!");
    }

    public static WorkerException WorkerNotExistsException()
    {
        return new WorkerException("Worker doesn't exist in list of workers!");
    }

    public static WorkerException WorkerNameIsNullException()
    {
        return new WorkerException("Worker name is null!");
    }

    public static WorkerException InvalidAccessLevelException()
    {
        return new WorkerException("Invalid access level!");
    }

    public static WorkerException DeviceIsNullException()
    {
        return new WorkerException("Device is null!");
    }

    public static WorkerException DeviceNameIsNullException()
    {
        return new WorkerException("Device name is null!");
    }

    public static WorkerException DeviceIsOccupiedException()
    {
        return new WorkerException("Device is occupied!");
    }

    public static WorkerException PathIsNullException()
    {
        return new WorkerException("Path is null!");
    }

    public static WorkerException DirectorIsNullException()
    {
        return new WorkerException("Director is null!");
    }

    public static WorkerException AccessLevelIsLowException()
    {
        return new WorkerException("Your access level is low!");
    }
}

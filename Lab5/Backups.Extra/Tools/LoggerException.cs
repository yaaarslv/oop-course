namespace Backups.Extra.Tools;

public class LoggerException : Exception
{
    private LoggerException(string message)
        : base(message) { }

    public static LoggerException LoggerIsNullException()
    {
        return new LoggerException("Logger is null!");
    }

    public static LoggerException LogFilePathIsNull()
    {
        return new LoggerException("Log file path is null!");
    }

    public static LoggerException LogFileNameIsNull()
    {
        return new LoggerException("Log file name is null!");
    }
}
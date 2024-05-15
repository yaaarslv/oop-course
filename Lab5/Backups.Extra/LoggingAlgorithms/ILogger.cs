namespace Backups.Extra.LoggingAlgorithms;

public interface ILogger
{
    void Log(string message);
    void EnablePrefix();
    void DisablePrefix();
}
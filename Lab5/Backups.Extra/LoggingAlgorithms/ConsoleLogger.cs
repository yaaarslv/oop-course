using System.Globalization;

namespace Backups.Extra.LoggingAlgorithms;

public class ConsoleLogger : ILogger
{
    public ConsoleLogger()
    {
        EnabledPrefix = true;
    }

    public bool EnabledPrefix { get; private set; }
    public void Log(string message)
    {
        string prefix = string.Empty;
        if (EnabledPrefix)
        {
            prefix = DateTime.Now.ToString(CultureInfo.CurrentCulture) + " ";
        }

        Console.WriteLine($"{prefix}{message}");
    }

    public void EnablePrefix()
    {
        EnabledPrefix = true;
    }

    public void DisablePrefix()
    {
        EnabledPrefix = false;
    }
}
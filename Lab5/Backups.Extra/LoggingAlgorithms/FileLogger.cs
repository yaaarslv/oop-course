using System.Globalization;
using Backups.Extra.Tools;

namespace Backups.Extra.LoggingAlgorithms;

public class FileLogger : ILogger
{
    public FileLogger(string path, string fileName)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw LoggerException.LogFilePathIsNull();
        }

        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw LoggerException.LogFileNameIsNull();
        }

        Path = path;
        FileName = fileName;
        EnabledPrefix = true;
    }

    public string FileName { get; }
    public string Path { get; }
    public bool EnabledPrefix { get; private set; }
    public void Log(string message)
    {
        string prefix = string.Empty;
        if (EnabledPrefix)
        {
            prefix = DateTime.Now.ToString(CultureInfo.CurrentCulture) + " ";
        }

        Directory.CreateDirectory(Path);
        File.AppendAllText($"{Path}{System.IO.Path.DirectorySeparatorChar}{FileName}", $"{prefix}{message}\n");
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
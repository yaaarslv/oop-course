using Backups.Tools;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string path, string name)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw BackupObjectException.PathIsNullException();
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw BackupObjectException.NameIsNullException();
        }

        Path = path;
        Name = name;
    }

    public string Path { get; }
    public string Name { get; }
}
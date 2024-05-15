using System.Text.Json;
using Backups.Extra.Tools;

namespace Backups.Extra.Entities;

public class Backup
{
    private List<BackupTaskExtra>? _backupTaskExtras;
    public Backup(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw BackupException.PathIsNull();
        }

        Path = path;
        _backupTaskExtras = new List<BackupTaskExtra>();
    }

    public string Path { get; }
    public IReadOnlyCollection<BackupTaskExtra>? BackupTaskExtras => _backupTaskExtras;
    public void SaveConfiguration()
    {
        FileStream fileStream = new FileStream($"{Path}{System.IO.Path.DirectorySeparatorChar}configuration.json", FileMode.OpenOrCreate);
        var options = new JsonSerializerOptions { WriteIndented = true };
        JsonSerializer.Serialize(fileStream, this, options);
        fileStream.Close();
    }

    public async void LoadConfiguration()
    {
        FileStream fileStream = new FileStream($"{Path}{System.IO.Path.DirectorySeparatorChar}configuration.json", FileMode.OpenOrCreate);
        Backup? backup = await JsonSerializer.DeserializeAsync<Backup>(fileStream);
        _backupTaskExtras = backup?._backupTaskExtras;
        fileStream.Close();
    }

    public BackupTaskExtra AddBackupTaskExtra(BackupTaskExtra backupTaskExtra)
    {
        if (backupTaskExtra is null)
        {
            throw BackupTaskExtraException.BackupTaskExtraIsNull();
        }

        _backupTaskExtras?.Add(backupTaskExtra);
        return backupTaskExtra;
    }

    public void RemoveBackupTaskExtra(BackupTaskExtra backupTaskExtra)
    {
        if (backupTaskExtra is null)
        {
            throw BackupTaskExtraException.BackupTaskExtraIsNull();
        }

        _backupTaskExtras?.Remove(backupTaskExtra);
    }
}
namespace Backups.Entities;

public class Archiver
{
    public static void SaveArchive(IReadOnlyCollection<Storage> storages, DirectoryInfo directoryInfo)
    {
        foreach (var storage in storages)
        {
            storage.Archive.Save(@$"{directoryInfo.FullName}{Path.DirectorySeparatorChar}{storage.StorageName}.zip");
        }
    }
}
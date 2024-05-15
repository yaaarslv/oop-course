using Backups.Entities;

namespace Backups.Extra.Models;

public class ArchiverExtra : Archiver
{
    public static void RestoreFile(Storage storage, DirectoryInfo directoryInfo)
    {
        storage.Archive.ExtractAll(directoryInfo.FullName);
    }
}
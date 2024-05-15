using Backups.Algorithms;
using Backups.Entities;
using Backups.Tools;
using Xunit;

namespace Backups.Test;

public class BackupsTest
{
    [Fact]
    public void Test1()
    {
        Repository repository = new Repository("/home/runner/work/yaaarslv/test1");
        BackupTask backupTask = new BackupTask(repository, new SplitStorage());
        BackupObject backupObject1 = new BackupObject("/bin/apt", "apt");
        BackupObject backupObject2 = new BackupObject("/bin/gzip", "gzip");
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        backupTask.SaveNewBackup();
        backupTask.RemoveBackupObject(backupObject2);
        backupTask.SaveNewBackup();
        Assert.True(backupTask.RestorePoints.Count == 2);
        Assert.True(Directory.EnumerateFiles($"/home/runner/work/yaaarslv/test1/Restore_point-{DateTime.Now.ToShortDateString()}").Count() + Directory.EnumerateFiles($"/home/runner/work/yaaarslv/test1/Restore_point-{DateTime.Now.ToShortDateString()}-1").Count() == 3);
    }

    [Fact]
    public void Test2()
    {
        Repository repository = new Repository("/home/runner/work/yaaarslv/test2");
        BackupTask backupTask = new BackupTask(repository, new SingleStorage());
        BackupObject backupObject1 = new BackupObject("/bin/apt", "apt");
        BackupObject backupObject2 = new BackupObject("/bin/gzip", "gzip");
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        backupTask.SaveNewBackup();
        Assert.True(backupTask.RestorePoints.Count == 1);
        Assert.True(Directory.EnumerateDirectories("/home/runner/work/yaaarslv/test2").Count() == 1);
        Assert.True(Directory.EnumerateFiles($"/home/runner/work/yaaarslv/test2/Restore_point-{DateTime.Now.ToShortDateString()}").First() == $"/home/runner/work/yaaarslv/test2/Restore_point-{DateTime.Now.ToShortDateString()}/Group_archive.zip");
    }
}
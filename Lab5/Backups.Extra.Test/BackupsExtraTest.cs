using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.ClearAlgorithms;
using Backups.Extra.Entities;
using Backups.Extra.LoggingAlgorithms;
using Xunit;

namespace Backups.Extra.Test;

public class BackupsExtraTest
{
    [Fact]
    public void ClearRestorePointsByCount()
    {
        BackupTaskExtra backupTaskExtra = new BackupTaskExtra(new Repository("/home/runner/work/yaaarslv/test1"), new SingleStorage(), new ConsoleLogger());
        BackupObject backupObject = new BackupObject("/bin/apt", "ahaha");
        backupTaskExtra.AddBackupObject(backupObject);
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.ClearRestorePoints(new ClearByCount(2));
        Assert.True(backupTaskExtra.RestorePoints.Count == 2);
    }

    [Fact]
    public void ClearRestorePointsByDate()
    {
        BackupTaskExtra backupTaskExtra = new BackupTaskExtra(new Repository("/home/runner/work/yaaarslv/test1"), new SingleStorage(), new ConsoleLogger());
        BackupObject backupObject = new BackupObject("/bin/apt", "ahaha");
        backupTaskExtra.AddBackupObject(backupObject);
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.SaveNewBackup();
        DateTime dt = DateTime.Now;
        Thread.Sleep(2000);
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.ClearRestorePoints(new ClearByDate(dt));
        Assert.True(backupTaskExtra.RestorePoints.Count == 1);
    }

    [Fact]
    public void ClearRestorePointsByHybridAtLeastOne()
    {
        BackupTaskExtra backupTaskExtra = new BackupTaskExtra(new Repository("/home/runner/work/yaaarslv/test1"), new SingleStorage(), new ConsoleLogger());
        BackupObject backupObject = new BackupObject("/bin/apt", "ahaha");
        backupTaskExtra.AddBackupObject(backupObject);
        backupTaskExtra.SaveNewBackup();
        DateTime dt = DateTime.Now;
        Thread.Sleep(2000);
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.ClearRestorePoints(new ClearByHybridAtLeastOne(new ClearByCount(3), new ClearByDate(dt)));
        Assert.True(backupTaskExtra.RestorePoints.Count == 2);
    }

    [Fact]
    public void ClearRestorePointsByHybridAll()
    {
        BackupTaskExtra backupTaskExtra = new BackupTaskExtra(new Repository("/home/runner/work/yaaarslv/test1"), new SingleStorage(), new ConsoleLogger());
        BackupObject backupObject = new BackupObject("/bin/apt", "ahaha");
        backupTaskExtra.AddBackupObject(backupObject);
        backupTaskExtra.SaveNewBackup();
        DateTime dt = DateTime.Now;
        Thread.Sleep(2000);
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.ClearRestorePoints(new ClearByHybridAll(new ClearByCount(1), new ClearByDate(dt)));
        Assert.True(backupTaskExtra.RestorePoints.Count == 2);
    }

    [Fact]
    public void MergeRestorePoints()
    {
        BackupTaskExtra backupTaskExtra = new BackupTaskExtra(new Repository("/home/runner/work/yaaarslv/test1"), new SplitStorage(), new ConsoleLogger());
        BackupObject backupObject1 = new BackupObject("/bin/apt", "ahaha");
        BackupObject backupObject2 = new BackupObject("/bin/gzip", "neahaha");
        backupTaskExtra.AddBackupObject(backupObject1);
        backupTaskExtra.AddBackupObject(backupObject2);
        backupTaskExtra.SaveNewBackup();
        backupTaskExtra.RemoveBackupObject(backupObject2);
        backupTaskExtra.SaveNewBackup();
        var merged = backupTaskExtra.Merge(backupTaskExtra.RestorePoints.First(), backupTaskExtra.RestorePoints.Last());
        Assert.Contains(backupObject1, merged.BackupObjects);
        Assert.True(merged.BackupObjects.Count == 1);
    }
}
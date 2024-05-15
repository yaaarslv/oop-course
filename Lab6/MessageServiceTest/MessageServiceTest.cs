using BusinessLayer.Services;
using DataAccessLayer.DataBases;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;
using DataAccessLayer.Tools;
using Xunit;

namespace MessageServiceTest;

public class MessageServiceTest
{
    private MessageService _service;
    public MessageServiceTest()
    {
        _service = new MessageService();
    }
    [Fact]
    public void AddWorker()
    {
        var worker = new Worker("Nu ya", 3);
        WorkersDataBase.GetInstance().AddWorker(worker, "login", "password");
        Assert.Contains(worker, WorkersDataBase.GetInstance().Workers);
    }

    [Fact]
    public void UpdateAccessLevel()
    {
        var worker = new Worker("Nu ya", 3);
        WorkersDataBase.GetInstance().AddWorker(worker, "login", "password");
        worker.SetAccessLevel(7);
        Assert.True(worker.AccessLevel == 7);
    }

    [Fact]
    public void AddMessage_MessageContains()
    {
        var message = new Message("test message", new MessageSender("nu ya"));
        _service.AddMessage(message);
        Assert.Contains(message, MessagesDataBase.GetInstance().AllMessages[message.SendTime.Date]);
    }

    [Fact]
    public void ReadMessage()
    {
        var message = new Message("test message", new MessageSender("nu ya"));
        _service.AddMessage(message);
        _service.Read(message);
        Assert.True(message.IsRead);
        Assert.True(message.Status == "Processed");
    }

    [Fact]
    public void AccessLevelIsLow()
    {
        MessagesDataBase.GetInstance().AddSource(new MessageSource(5, "@qwerty"));
        var message = new Message("test message", new MessageSender("nu ya"));
        _service.AddMessage(message);
        var worker = new Worker("Nu ya", 3);
        WorkersDataBase.GetInstance().AddWorker(worker, "login", "password");
        var messages = _service.GetMessages(worker);
        Assert.True(messages.Count == 0);
    }

    [Fact]
    public void MakeReport_AccessLevelIsLow_ThrowException()
    {
        var worker = new Worker("Nu ya", 3);
        WorkersDataBase.GetInstance().AddWorker(worker, "login", "password");
        Assert.Throws<WorkerException>(() =>
        {
            _service.MakeReport(worker, DateTime.Now.AddDays(-2), DateTime.Now);
        });
    }
}
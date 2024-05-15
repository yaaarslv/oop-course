using DataAccessLayer.DataBases;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;
using DataAccessLayer.Tools;

namespace BusinessLayer.Services;

public class MessageService
{
    public MessageService()
    { }

    public void Read(IMessage message)
    {
        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        message.Read();
        message.ChangeToProcessed();
    }

    public void Reply(IMessage message, string replyMessage)
    {
        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        message.Reply(replyMessage);
        message.ChangeToProcessed();
    }

    public IMessage AddMessage(IMessage message)
    {
        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        MessagesDataBase.GetInstance().AddMessage(message);
        return message;
    }

    public IReadOnlyCollection<IMessage> GetMessages(Worker worker)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        var messages = MessagesDataBase.GetInstance().MessageSources.Where(s => s.AccessLevel <= worker.AccessLevel)
            .SelectMany(s => s.GetMessages()).ToList();
        messages.ForEach(m => worker.Device.AddMessage(m));
        return messages;
    }

    public void CloseSession(Worker worker)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        worker.Device.RemoveAllMessages();
        worker.Device.ChangeToFree();
    }

    public Report MakeReport(Worker worker, DateTime dateTime1, DateTime dateTime2)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        if (worker.AccessLevel != WorkersDataBase.MaxAccessLevel)
        {
            throw WorkerException.AccessLevelIsLowException();
        }

        Report report = WorkersDataBase.GetInstance().Director.MakeReport(dateTime1, dateTime2);
        ReportDataBase.GetInstance().AddReport(DateTime.Now, report);
        return report;
    }

    public string PrintReport(Report report)
    {
        if (report is null)
        {
            throw ReportException.ReportIsNullException();
        }

        string date = $"For {report.CreationDate},\n";
        string byWorker = $"{report.ProcessedMessages} messages were processed\n";
        string byDevice = report.DeviceMessages.Keys.Select(device => $"The device {device.Name} received {report.DeviceMessages[device]} messages\n").Aggregate(string.Empty, (current, deviceMessages) => current + deviceMessages);
        string statistics = $"During the interval from {report.StartDate} to {report.EndDate}, there were {report.DateStatistics} messages\n";
        string result = date + byWorker + byDevice + statistics;
        Console.WriteLine(result);
        return result;
    }

    public void SaveConfiguration(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw WorkerException.PathIsNullException();
        }

        MessagesDataBase.GetInstance().SaveConfiguration(path);
        ReportDataBase.GetInstance().SaveConfiguration(path);
        WorkersDataBase.GetInstance().SaveConfiguration(path);
    }

    public void LoadConfiguration(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw WorkerException.PathIsNullException();
        }

        MessagesDataBase.GetInstance().LoadConfiguration(path);
        ReportDataBase.GetInstance().LoadConfiguration(path);
        WorkersDataBase.GetInstance().LoadConfiguration(path);
    }
}
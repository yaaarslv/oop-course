using DataAccessLayer.DataBases;
using DataAccessLayer.FilterAlgorithms;

namespace DataAccessLayer.Entities;

public class Report
{
    private readonly Dictionary<Device, int> _deviceMessages;
    public Report(DateTime dateTime1, DateTime dateTime2)
    {
        _deviceMessages = new Dictionary<Device, int>();
        StartDate = dateTime1.Date;
        EndDate = dateTime2.Date;

        var byWorker = new ByWorker();
        var byDevice = new ByDevice();
        var statistics = new Statistics();

        ProcessedMessages = byWorker.CountProcessedMessages(dateTime1, dateTime2);
        foreach (var device in MessagesDataBase.GetInstance().DeviceMessages.Keys)
        {
            var count = byDevice.CountProcessedMessages(dateTime1, dateTime2, device);
            _deviceMessages.Add(device, count);
        }

        DateStatistics = statistics.CountProcessedMessages(dateTime1, dateTime2);
        CreationDate = DateTime.Now.Date;
    }

    public int ProcessedMessages { get; }
    public IReadOnlyDictionary<Device, int> DeviceMessages => _deviceMessages;
    public int DateStatistics { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public DateTime CreationDate { get; }
}
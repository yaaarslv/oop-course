using DataAccessLayer.DataBases;
using DataAccessLayer.Entities;
using DataAccessLayer.Tools;

namespace DataAccessLayer.FilterAlgorithms;

public class ByDevice : IAlgorithm
{
    public ByDevice()
    { }

    public int CountProcessedMessages(DateTime dateTime1, DateTime dateTime2, Device device)
    {
        if (device is null)
        {
            throw WorkerException.DeviceIsNullException();
        }

        return MessagesDataBase.GetInstance().DeviceMessages[device][dateTime1.Date].Count;
    }
}
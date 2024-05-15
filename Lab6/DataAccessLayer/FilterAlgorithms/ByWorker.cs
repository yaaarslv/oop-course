using DataAccessLayer.DataBases;
using DataAccessLayer.Entities;

namespace DataAccessLayer.FilterAlgorithms;

public class ByWorker : IAlgorithm
{
    public ByWorker()
    { }

    public int CountProcessedMessages(DateTime dateTime1, DateTime dateTime2, Device device = null)
    {
        return MessagesDataBase.GetInstance().AllMessages[DateTime.Now.Date].Count(messages => messages.Status == "Processed");
    }
}
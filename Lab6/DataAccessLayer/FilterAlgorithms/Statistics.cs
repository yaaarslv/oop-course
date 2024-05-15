using DataAccessLayer.DataBases;
using DataAccessLayer.Entities;

namespace DataAccessLayer.FilterAlgorithms;

public class Statistics : IAlgorithm
{
    public int CountProcessedMessages(DateTime dateTime1, DateTime dateTime2, Device device = null)
    {
        return MessagesDataBase.GetInstance().AllMessages.Keys.Where(date => CheckDateExistence(date, dateTime1, dateTime2))
            .Sum(date => MessagesDataBase.GetInstance().AllMessages[date].Count);
    }

    private bool CheckDateExistence(DateTime dateTime, DateTime startDateTime, DateTime endDateTime)
    {
        return dateTime >= startDateTime && dateTime <= endDateTime;
    }
}
using DataAccessLayer.Entities;

namespace DataAccessLayer.FilterAlgorithms;

public interface IAlgorithm
{
    int CountProcessedMessages(DateTime dateTime1, DateTime dateTime2, Device device);
}
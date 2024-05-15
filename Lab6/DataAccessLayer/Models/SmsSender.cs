using DataAccessLayer.Tools;

namespace DataAccessLayer.Models;

public class SmsSender : ISender
{
    public SmsSender(string senderInfo)
    {
        if (string.IsNullOrWhiteSpace(senderInfo))
        {
            throw SenderException.SenderInfoIsNull();
        }

        SenderInfo = senderInfo;
    }

    public string SenderInfo { get; }
}
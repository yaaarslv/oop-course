using DataAccessLayer.Tools;

namespace DataAccessLayer.Models;

public class MessageSender : ISender
{
    public MessageSender(string senderInfo)
    {
        if (string.IsNullOrWhiteSpace(senderInfo))
        {
            throw SenderException.SenderInfoIsNull();
        }

        SenderInfo = senderInfo;
    }

    public string SenderInfo { get; }
}
using DataAccessLayer.Tools;

namespace DataAccessLayer.Models;

public class EmailSender : ISender
{
    public EmailSender(string senderInfo)
    {
        if (string.IsNullOrWhiteSpace(senderInfo))
        {
            throw SenderException.SenderInfoIsNull();
        }

        SenderInfo = senderInfo;
    }

    public string SenderInfo { get; }
}
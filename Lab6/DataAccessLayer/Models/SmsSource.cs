using DataAccessLayer.Tools;

namespace DataAccessLayer.Models;

public class SmsSource : IMessageSource
{
    private readonly List<IMessage> _sms;
    public SmsSource(int accessLevel, string sourceInfo)
    {
        if (accessLevel < 0)
        {
            throw WorkerException.InvalidAccessLevelException();
        }

        if (string.IsNullOrWhiteSpace(sourceInfo))
        {
            throw SourceException.SourceInfoIsNullException();
        }

        _sms = new List<IMessage>();
        AccessLevel = accessLevel;
        SourceInfo = sourceInfo;
    }

    public IReadOnlyCollection<IMessage> Sms => _sms;
    public string SourceInfo { get; }
    public int AccessLevel { get; }

    public void AddMessage(IMessage message)
    {
        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        if (message.GetType() == new Sms("Example sms", new EmailSender("+78005553535")).GetType())
        {
            _sms.Add(message);
        }
    }

    public IReadOnlyCollection<IMessage> GetMessages()
    {
        return Sms;
    }
}
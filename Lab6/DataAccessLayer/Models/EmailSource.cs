using DataAccessLayer.Tools;

namespace DataAccessLayer.Models;

public class EmailSource : IMessageSource
{
    private readonly List<IMessage> _emails;
    public EmailSource(int accessLevel, string sourceInfo)
    {
        if (accessLevel < 0)
        {
            throw WorkerException.InvalidAccessLevelException();
        }

        if (string.IsNullOrWhiteSpace(sourceInfo))
        {
            throw SourceException.SourceInfoIsNullException();
        }

        _emails = new List<IMessage>();
        AccessLevel = accessLevel;
        SourceInfo = sourceInfo;
    }

    public IReadOnlyCollection<IMessage> Emails => _emails;
    public string SourceInfo { get; }
    public int AccessLevel { get; }

    public void AddMessage(IMessage message)
    {
        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        if (message.GetType() == new Email("Example email", new EmailSender("339009@niuitmo.ru")).GetType())
        {
            _emails.Add(message);
        }
    }

    public IReadOnlyCollection<IMessage> GetMessages()
    {
        return Emails;
    }
}
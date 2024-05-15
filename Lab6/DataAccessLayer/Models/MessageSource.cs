using DataAccessLayer.Tools;

namespace DataAccessLayer.Models;

public class MessageSource : IMessageSource
{
    private readonly List<IMessage> _messages;
    public MessageSource(int accessLevel, string sourceInfo)
    {
        if (accessLevel < 0)
        {
            throw WorkerException.InvalidAccessLevelException();
        }

        if (string.IsNullOrWhiteSpace(sourceInfo))
        {
            throw SourceException.SourceInfoIsNullException();
        }

        _messages = new List<IMessage>();
        AccessLevel = accessLevel;
        SourceInfo = sourceInfo;
    }

    public IReadOnlyCollection<IMessage> Messages => _messages;
    public string SourceInfo { get; }
    public int AccessLevel { get; }

    public void AddMessage(IMessage message)
    {
        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        if (message.GetType() == new Message("Example message", new MessageSender("@yaaarsl_v")).GetType())
        {
            _messages.Add(message);
        }
    }

    public IReadOnlyCollection<IMessage> GetMessages()
    {
        return Messages;
    }
}
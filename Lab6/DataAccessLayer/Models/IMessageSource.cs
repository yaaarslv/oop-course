namespace DataAccessLayer.Models;

public interface IMessageSource
{
    string SourceInfo { get; }
    int AccessLevel { get; }
    void AddMessage(IMessage message);
    IReadOnlyCollection<IMessage> GetMessages();
}
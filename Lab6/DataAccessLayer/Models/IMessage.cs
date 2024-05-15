namespace DataAccessLayer.Models;

public interface IMessage
{
    ISender Sender { get; }
    DateTime SendTime { get; }
    string Text { get; }
    string Status { get; }
    bool IsRead { get; }
    bool IsReplied { get; }
    void ChangeToRead();
    void ChangeToReplied();
    void ChangeToReceived();
    void ChangeToProcessed();
    void Read();
    void Reply(string message);
}
using DataAccessLayer.Tools;

namespace DataAccessLayer.Models;

public class Message : IMessage
{
    public Message(string text, ISender sender)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw MessageException.TextIsNullException();
        }

        if (sender is null)
        {
            throw SenderException.SenderIsNullException();
        }

        Text = text;
        SendTime = DateTime.Now;
        Status = "New";
        Sender = sender;
        IsRead = false;
        IsReplied = false;
    }

    public ISender Sender { get; }
    public DateTime SendTime { get; }
    public string Text { get; }
    public string Status { get; private set; }
    public bool IsRead { get; private set; }
    public bool IsReplied { get; private set; }
    public void ChangeToRead()
    {
        IsRead = true;
    }

    public void ChangeToReplied()
    {
        ChangeToRead();
        IsReplied = true;
    }

    public void ChangeToReceived()
    {
        if (Status == "Processed" || Status == "Received")
        {
            throw MessageException.ActionAlreadyDoneException();
        }

        Status = "Received";
    }

    public void ChangeToProcessed()
    {
        if (Status == "Processed" || Status == "Received")
        {
            throw MessageException.ActionAlreadyDoneException();
        }

        Status = "Processed";
    }

    public void Read()
    {
        ChangeToRead();
    }

    public void Reply(string message)
    {
        Console.WriteLine(message);
        ChangeToReplied();
    }
}
using DataAccessLayer.DataBases;
using DataAccessLayer.Models;
using DataAccessLayer.Tools;

namespace DataAccessLayer.Entities;

public class Device
{
    private readonly List<IMessage> _messages;

    public Device(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw WorkerException.DeviceNameIsNullException();
        }

        _messages = new List<IMessage>();
        Name = name;
    }

    public IReadOnlyCollection<IMessage> Messages => _messages;
    public bool IsFree { get; private set; }
    public string Name { get; }

    public IMessage AddMessage(IMessage message)
    {
        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        if (_messages.Contains(message))
        {
            throw MessageException.MessageAlreadyExistsException();
        }

        _messages.Add(message);
        message.ChangeToReceived();
        MessagesDataBase.GetInstance().AddMessage(this, message);
        return message;
    }

    public void RemoveMessage(IMessage message)
    {
        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        if (!_messages.Contains(message))
        {
            throw MessageException.MessageNotExistsException();
        }

        _messages.Remove(message);
    }

    public void RemoveAllMessages()
    {
        _messages.Clear();
    }

    public void ChangeToOccupied()
    {
        IsFree = false;
    }

    public void ChangeToFree()
    {
        IsFree = true;
    }
}
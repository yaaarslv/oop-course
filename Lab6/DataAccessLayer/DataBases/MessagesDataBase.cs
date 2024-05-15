using System.Text.Json;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;
using DataAccessLayer.Tools;

namespace DataAccessLayer.DataBases;

public class MessagesDataBase
{
    private static MessagesDataBase _instance;
    private Dictionary<DateTime, List<IMessage>> _allMessages;
    private Dictionary<Device, Dictionary<DateTime, List<IMessage>>> _deviceMessages;
    private List<IMessageSource> _messageSources;

    private MessagesDataBase()
    {
        _allMessages = new Dictionary<DateTime, List<IMessage>>();
        _deviceMessages = new Dictionary<Device, Dictionary<DateTime, List<IMessage>>>();
        _messageSources = new List<IMessageSource>();
    }

    public IReadOnlyDictionary<DateTime, List<IMessage>> AllMessages => _allMessages;
    public IReadOnlyDictionary<Device, Dictionary<DateTime, List<IMessage>>> DeviceMessages => _deviceMessages;
    public IReadOnlyCollection<IMessageSource> MessageSources => _messageSources;

    public static MessagesDataBase GetInstance()
    {
        if (_instance is null)
        {
            _instance = new MessagesDataBase();
        }

        return _instance;
    }

    public IMessage AddMessage(IMessage message)
    {
        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        _messageSources.ForEach(m => m.AddMessage(message));
        if (!_allMessages.ContainsKey(message.SendTime.Date))
        {
            _allMessages.Add(message.SendTime.Date, new List<IMessage>());
        }

        _allMessages[message.SendTime.Date].Add(message);
        return message;
    }

    public IMessage AddMessage(Device device, IMessage message)
    {
        if (device is null)
        {
            throw WorkerException.DeviceIsNullException();
        }

        if (message is null)
        {
            throw MessageException.MessageIsNullException();
        }

        _deviceMessages[device][message.SendTime.Date].Add(message);
        return message;
    }

    public IMessageSource AddSource(IMessageSource source)
    {
        if (source is null)
        {
            throw SourceException.SourceIsNullException();
        }

        if (CheckSourceExistence(source))
        {
            throw SourceException.SourceAlreadyExistsException();
        }

        _messageSources.Add(source);
        return source;
    }

    public void SaveConfiguration(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw WorkerException.PathIsNullException();
        }

        FileStream fileStream = new FileStream($"{path}{Path.DirectorySeparatorChar}messagesDatabase_configuration.json", FileMode.OpenOrCreate);
        var options = new JsonSerializerOptions { WriteIndented = true };
        JsonSerializer.Serialize(fileStream, this, options);
        fileStream.Close();
    }

    public async void LoadConfiguration(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw WorkerException.PathIsNullException();
        }

        FileStream fileStream = new FileStream($"{path}{Path.DirectorySeparatorChar}messagesDatabase_configuration.json", FileMode.OpenOrCreate);
        MessagesDataBase messagesDataBase = await JsonSerializer.DeserializeAsync<MessagesDataBase>(fileStream);
        _allMessages = messagesDataBase?._allMessages;
        _deviceMessages = messagesDataBase?._deviceMessages;
        _messageSources = messagesDataBase?._messageSources;
        fileStream.Close();
    }

    private bool CheckSourceExistence(IMessageSource source)
    {
        return _messageSources.Any(s => s.SourceInfo == source.SourceInfo);
    }
}
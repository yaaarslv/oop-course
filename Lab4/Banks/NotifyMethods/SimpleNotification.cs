namespace Banks.NotifyMethods;

public class SimpleNotification : INotificator
{
    public bool IsNotified { get; private set; }
    public void Notify(string message)
    {
        Console.WriteLine(message);
        IsNotified = true;
    }
}
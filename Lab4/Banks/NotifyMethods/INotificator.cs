namespace Banks.NotifyMethods;

public interface INotificator
{
    bool IsNotified { get; }
    void Notify(string message);
}
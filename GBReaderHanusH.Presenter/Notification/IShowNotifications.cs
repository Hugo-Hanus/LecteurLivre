namespace Presenter.Notification
{
    public interface IShowNotifications
    {
        void Push(NotificationSeverity serverity, string title, string message);
    }
}
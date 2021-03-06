namespace UI.Notifications
{
    public interface INotificationService
    {
        void ShowSuccessMessage(string message);

        void ShowErrorMessage(string message);

        void ShowWarningMessage(string message);

        void ShowInfoMessage(string message);
    }
}
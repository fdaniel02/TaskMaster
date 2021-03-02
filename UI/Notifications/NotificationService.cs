using Prism.Events;
using UI.Notifications.Enums;
using UI.Notifications.Events;
using UI.Notifications.Models;

namespace UI.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IEventAggregator _eventAggregator;

        public NotificationService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void ShowSuccessMessage(string message)
        {
            ShowNotification(message, NotificationTypes.Success);
        }

        private void ShowNotification(string message, NotificationTypes type)
        {
            var notification = new NotificationModel
            {
                Text = message,
                Type = type,
            };

            _eventAggregator.GetEvent<ShowNotificationEvent>().Publish(notification);
        }
    }
}
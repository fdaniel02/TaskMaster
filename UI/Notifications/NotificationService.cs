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

        public void ShowSuccess(string text)
        {
            ShowNotification(text, NotificationTypes.Success);
        }

        private void ShowNotification(string text, NotificationTypes type)
        {
            var notification = new NotificationModel
            {
                Text = text,
                Type = type,
            };

            _eventAggregator.GetEvent<ShowNotificationEvent>().Publish(notification);
        }
    }
}
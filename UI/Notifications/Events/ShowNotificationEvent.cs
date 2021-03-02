using Prism.Events;
using UI.Notifications.Models;

namespace UI.Notifications.Events
{
    public class ShowNotificationEvent : PubSubEvent<NotificationModel>
    {
    }
}
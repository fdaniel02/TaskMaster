using UI.Notifications.Enums;

namespace UI.Notifications.Models
{
    public class NotificationModel
    {
        public string Text { get; set; }

        public NotificationTypes Type { get; set; }
    }
}
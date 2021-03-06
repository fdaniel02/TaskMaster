using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using UI.Notifications.Enums;

namespace UI.Notifications.Converters
{
    public class NotificationColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush();
            if (value is not NotificationTypes notificationType)
            {
                brush.Color = Color.FromRgb(99, 192, 223);
                return brush;
            }

            brush.Color = ConvertTypeToColor(notificationType);

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Color ConvertTypeToColor(NotificationTypes priority) =>
           priority switch
           {
               NotificationTypes.Error => Color.FromRgb(207, 0, 15),
               NotificationTypes.Warning => Color.FromRgb(240, 84, 30),
               NotificationTypes.Success => Color.FromRgb(0, 153, 68),
               _ => Color.FromRgb(99, 192, 223),
           };
    }
}
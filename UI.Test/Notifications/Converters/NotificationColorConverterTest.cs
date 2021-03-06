using System.Windows.Media;
using FluentAssertions;
using UI.Notifications.Converters;
using UI.Notifications.Enums;
using Xunit;

namespace UI.Test.Notifications.Converters
{
    public class NotificationColorConverterTest
    {
        [Theory]
        [InlineData(NotificationTypes.Error, 207, 0, 15)]
        [InlineData(NotificationTypes.Warning, 240, 84, 30)]
        [InlineData(NotificationTypes.Success, 0, 153, 68)]
        [InlineData(NotificationTypes.Info, 99, 192, 223)]
        [InlineData((NotificationTypes)int.MaxValue, 99, 192, 223)]
        public void Convert_ReturnsColor(NotificationTypes type, byte r, byte g, byte b)
        {
            var sut = new NotificationColorConverter();

            var actual = sut.Convert(type, null, null, null) as SolidColorBrush;

            actual.Color.R.Should().Be(r);
            actual.Color.G.Should().Be(g);
            actual.Color.B.Should().Be(b);
        }

        [Fact]
        public void Convert_InputNull_ReturnsInfoColor()
        {
            var sut = new NotificationColorConverter();

            var actual = sut.Convert(null, null, null, null) as SolidColorBrush;

            actual.Color.R.Should().Be(99);
            actual.Color.G.Should().Be(192);
            actual.Color.B.Should().Be(223);
        }

        [Fact]
        public void Convert_InputNotNotificationType_ReturnsInfoColor()
        {
            var sut = new NotificationColorConverter();

            var actual = sut.Convert("Something", null, null, null) as SolidColorBrush;

            actual.Color.R.Should().Be(99);
            actual.Color.G.Should().Be(192);
            actual.Color.B.Should().Be(223);
        }
    }
}
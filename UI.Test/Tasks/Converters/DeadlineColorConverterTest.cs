using System.Windows.Media;
using FluentAssertions;
using UI.Tasks.Constants;
using UI.Tasks.Converters;
using Xunit;

namespace UI.Test.Tasks.Converters
{
    public class DeadlineColorConverterTest
    {
        [Fact]
        public void Convert_InputNotValidDate_ReturnsDefaultColor()
        {
            var expected = DeadlineColors.Default;

            var sut = new DeadlineColorConverter();

            var actual = sut.Convert(null, null, null, null) as SolidColorBrush;

            actual?.Color.Should().Be(expected);
        }

        [Fact]
        public void ConvertBack_ReturnsNull()
        {
            var sut = new DeadlineColorConverter();

            var actual = sut.ConvertBack(null, null, null, null);

            actual.Should().BeNull();
        }
    }
}
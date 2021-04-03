using System.Collections.Generic;
using System.Windows.Media;
using Domain.Enums;
using FluentAssertions;
using UI.Tasks.Constants;
using UI.Tasks.Converters;
using Xunit;

namespace UI.Test.Tasks.Converters
{
    public class PriorityColorConverterTest
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] { ProjectPriorities.Critical, PriorityColors.Critical };
            yield return new object[] { ProjectPriorities.High, PriorityColors.High };
            yield return new object[] { ProjectPriorities.Normal, PriorityColors.Normal };
            yield return new object[] { ProjectPriorities.Minor, PriorityColors.Minor };
            yield return new object[] { null, PriorityColors.Default };
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Convert_ReturnsCorrectColor(ProjectPriorities type, Color expected)
        {
            var sut = new PriorityColorConverter();

            var actual = sut.Convert(type, null, null, null) as SolidColorBrush;

            actual?.Color.Should().Be(expected);
        }

        [Fact]
        public void Convert_InputNotProjectPriorities_ReturnsDefaultColor()
        {
            var expected = PriorityColors.Default;

            var sut = new PriorityColorConverter();

            var actual = sut.Convert("", null, null, null) as SolidColorBrush;

            actual?.Color.Should().Be(expected);
        }

        [Fact]
        public void ConvertBack_ReturnsNull()
        {
            var sut = new PriorityColorConverter();

            var actual = sut.ConvertBack(null, null, null, null);

            actual.Should().BeNull();
        }
    }
}
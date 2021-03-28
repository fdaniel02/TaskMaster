using Domain.Enums;
using Domain.Models;
using FluentAssertions;
using UI.Tasks.Filters;
using Xunit;

namespace UI.Test.Tasks.Filters
{
    public class ProjectFilterTest
    {
        // TODO: tests needed for tagFilter. However these tests are too complex,
        // we need to separate the filters to write the tests
        [Theory]
        [InlineData("Test", ProjectStates.Next, "", "", false, true)]
        [InlineData("Test", ProjectStates.Closed, "", "", false, false)]
        [InlineData("Test", ProjectStates.Closed, "", "", true, true)]
        [InlineData("Test", ProjectStates.Closed, "A", "", true, false)]
        [InlineData("Test", ProjectStates.Active, "", "", false, true)]
        [InlineData("Test", ProjectStates.Active, "A", "", false, false)]
        [InlineData("Test", ProjectStates.Active, "Test", "", false, true)]
        [InlineData("Test", ProjectStates.Active, "Testa", "", false, false)]
        [InlineData("Test", ProjectStates.Active, "T est", "", false, false)]
        [InlineData("", ProjectStates.Active, "", "", false, true)]
        [InlineData("", ProjectStates.Active, "Test", "", false, false)]
        public void FilterProject_ReturnsFilteredProject(
            string name,
            ProjectStates state,
            string searchTerm,
            string tagFilter,
            bool showClosed,
            bool expected)
        {
            Project project = new()
            {
                Name = name,
                State = state,
            };

            var sut = new ProjectFilter();

            var actual = sut.FilterProject(project, searchTerm, tagFilter, showClosed);

            actual.Should().Be(expected);
        }
    }
}
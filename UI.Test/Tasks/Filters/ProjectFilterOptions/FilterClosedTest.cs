using System;
using Domain.Enums;
using Domain.Models;
using FluentAssertions;
using UI.Tasks.Filters;
using UI.Tasks.Filters.ProjectFilterOptions;
using Xunit;

namespace UI.Test.Tasks.Filters.ProjectFilterOptions
{
    public class FilterClosedTest
    {
        [Theory]
        [InlineData(ProjectStates.Closed, true, true)]
        [InlineData(ProjectStates.Active, true, true)]
        [InlineData(ProjectStates.Closed, false, false)]
        [InlineData(ProjectStates.Backlog, false, true)]
        public void Filter_ReturnsCorrectResult(ProjectStates state, bool showClosed, bool expected)
        {
            var project = new Project { State = state };
            var projectFilterArgs = new ProjectFilterArgs { ShowClosed = showClosed };

            var sut = new FilterClosed();

            var actual = sut.Filter(project, projectFilterArgs);

            actual.Should().Be(expected);
        }

        [Fact]
        public void Filter_ProjectNull_ThrowsArgumentNullException()
        {
            var projectFilterArgs = new ProjectFilterArgs();

            var sut = new FilterClosed();

            Assert.Throws<ArgumentNullException>(() => sut.Filter(null, projectFilterArgs));
        }

        [Fact]
        public void Filter_ProjectFilterArgsNull_ReturnsTrue()
        {
            var project = new Project { State = ProjectStates.Closed };

            var sut = new FilterClosed();
            sut.Filter(project, null);
        }
    }
}
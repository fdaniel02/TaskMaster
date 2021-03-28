using System.Collections.Generic;
using Domain.Models;
using FluentAssertions;
using Moq;
using UI.Tasks.Filters;
using UI.Tasks.Filters.ProjectFilterOptions;
using Xunit;

namespace UI.Test.Tasks.Filters
{
    public class ProjectFilterTest
    {
        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        public void FilterProject_ReturnsFilteredProject(bool filterResult1, bool filterResult2, bool expected)
        {
            var filterOptions = GetProjectFilterOptions(filterResult1, filterResult2);
            var project = new Project();
            var projectFilterArgs = new ProjectFilterArgs();

            var sut = new ProjectFilter(filterOptions);

            var act = sut.FilterProject(project, projectFilterArgs);

            act.Should().Be(expected);
        }

        private static List<IProjectFilterOption> GetProjectFilterOptions(bool result1, bool result2)
        {
            var filter1 = new Mock<IProjectFilterOption>();
            filter1.Setup(x => x.Filter(It.IsAny<Project>(), It.IsAny<ProjectFilterArgs>())).Returns(result1);

            var filter2 = new Mock<IProjectFilterOption>();
            filter2.Setup(x => x.Filter(It.IsAny<Project>(), It.IsAny<ProjectFilterArgs>())).Returns(result2);

            var filterOptions = new List<IProjectFilterOption> { filter1.Object, filter2.Object };
            return filterOptions;
        }
    }
}
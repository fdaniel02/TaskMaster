using System;
using Domain.Models;
using FluentAssertions;
using UI.Tasks.Filters;
using UI.Tasks.Filters.ProjectFilterOptions;
using Xunit;

namespace UI.Test.Tasks.Filters.ProjectFilterOptions
{
    public class FilterNameTest
    {
        [Theory]
        [InlineData("TestProject", "Test", true)]
        [InlineData("TestProject", "TestT", false)]
        [InlineData("TestProject", "", true)]
        [InlineData("", "", true)]
        [InlineData("", "Test", false)]
        [InlineData(null, "Test", false)]
        public void Filter_ReturnsCorrectResult(string projectName, string filter, bool expected)
        {
            var project = new Project { Name = projectName };
            var projectFilterArgs = new ProjectFilterArgs { Name = filter };

            var sut = new FilterName();

            var actual = sut.Filter(project, projectFilterArgs);

            actual.Should().Be(expected);
        }

        [Fact]
        public void Filter_ProjectNull_ThrowsArgumentNullException()
        {
            var projectFilterArgs = new ProjectFilterArgs();

            var sut = new FilterName();

            Assert.Throws<ArgumentNullException>(() => sut.Filter(null, projectFilterArgs));
        }

        [Fact]
        public void Filter_ProjectFilterArgsNull_ReturnsTrue()
        {
            var project = new Project { Name = "TestProject" };

            var sut = new FilterName();
            var actual = sut.Filter(project, null);

            actual.Should().Be(true);
        }
    }
}
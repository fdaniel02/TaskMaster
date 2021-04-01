using System;
using System.Collections.Generic;
using Domain.Models;
using FluentAssertions;
using UI.Tasks.Filters;
using UI.Tasks.Filters.ProjectFilterOptions;
using Xunit;

namespace UI.Test.Tasks.Filters.ProjectFilterOptions
{
    public class FilterTagTest
    {
        [Theory]
        [InlineData("Tag1", true)]
        [InlineData("Tagg1", false)]
        [InlineData("", true)]
        public void Filter_ReturnsCorrectResult(string filter, bool expected)
        {
            var project = new Project
            {
                ProjectTags = new List<ProjectTags>
                {
                    new()
                    {
                        Tag = new Tag
                        {
                            Name = "Tag1"
                        }
                    }
                }
            };

            var projectFilterArgs = new ProjectFilterArgs { Tag = filter };

            var sut = new FilterTag();

            var actual = sut.Filter(project, projectFilterArgs);

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("Tag1", false)]
        [InlineData("Tagg1", false)]
        [InlineData("", true)]
        public void Filter_NoTagOnProject_ReturnsCorrectResult(string filter, bool expected)
        {
            var project = new Project();

            var projectFilterArgs = new ProjectFilterArgs { Tag = filter };

            var sut = new FilterTag();

            var actual = sut.Filter(project, projectFilterArgs);

            actual.Should().Be(expected);
        }

        [Fact]
        public void Filter_ProjectNull_ThrowsArgumentNullException()
        {
            var projectFilterArgs = new ProjectFilterArgs();

            var sut = new FilterTag();

            Assert.Throws<ArgumentNullException>(() => sut.Filter(null, projectFilterArgs));
        }

        [Fact]
        public void Filter_ProjectFilterArgsNull_ReturnsTrue()
        {
            var project = new Project();

            var sut = new FilterTag();
            var actual = sut.Filter(project, null);

            actual.Should().Be(true);
        }
    }
}
using System.Collections.Generic;
using Domain.Models;
using FluentAssertions;
using Services.Test.Fakes;
using TaskMaster.Domain.Models;
using Xunit;

namespace Services.Test
{
    public class ProjectServiceTest
    {
        [Fact]
        public async void GetProjects_ReturnsAllProjects()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository);

            var actual = sut.GetProjects();

            actual.Should().Equal(testProjects);
        }

        private List<Project> SetupBasicProjectData()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = new ProjectState() { ID = 1 }, },
                new() { ID = 2, Name = "Test2", State = new ProjectState() { ID = 2 }, },
                new() { ID = 3, Name = "Test3", State = new ProjectState() { ID = 3 }, },
            };
        }
    }
}
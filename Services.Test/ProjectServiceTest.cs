using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using FluentAssertions;
using Services.Enums;
using Services.Test.Fakes;
using TaskMaster.Domain.Models;
using Xunit;

namespace Services.Test
{
    public class ProjectServiceTest
    {
        [Fact]
        public void GetProjects_ReturnsAllProjects()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository);
            var expected = testProjects;

            var actual = sut.GetProjects();

            actual.Should().Equal(expected);
        }

        [Fact]
        public void GetOpenProjects_ReturnOnlyOpenProjects()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository);
            var expected = testProjects.Where(p => p.State.ID != (int)ProjectStates.Closed);

            var actual = sut.GetOpenProjects();

            actual.Should().Equal(expected);
        }

        [Fact]
        public void AddNewProject_AddsNewProject()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository);
            var newProject = new Project() { ID = 4, };

            sut.AddNewProject(newProject);

            repository.Projects.Should().Contain(newProject);
        }

        [Fact]
        public void UpdateProject_UpdatesExistingProject()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository);
            var updatedProject = repository.Projects.First();

            updatedProject.Name = "Updated";

            sut.UpdateProject(updatedProject);

            repository.Projects.Should().Contain(updatedProject);
            repository.Projects.First().Name.Should().Be("Updated");
        }

        [Fact]
        public void SaveChanges_ProjectDoesntExists_AddsNewProject()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository);
            var newProject = new Project() { ID = 0, };

            sut.SaveChanges(newProject);

            repository.Projects.Should().Contain(newProject);
        }

        [Fact]
        public void SaveChanges_ProjectExists_UpdatesExistingProject()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository);
            var updatedProject = repository.Projects.First();

            updatedProject.Name = "Updated";

            sut.SaveChanges(updatedProject);

            repository.Projects.Should().Contain(updatedProject);
            repository.Projects.First().Name.Should().Be("Updated");
        }

        private List<Project> SetupBasicProjectData()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = new ProjectState() { ID = (int)ProjectStates.Inbox }, },
                new() { ID = 2, Name = "Test2", State = new ProjectState() { ID = (int)ProjectStates.Next }, },
                new() { ID = 3, Name = "Test3", State = new ProjectState() { ID = (int)ProjectStates.Closed }, },
            };
        }
    }
}
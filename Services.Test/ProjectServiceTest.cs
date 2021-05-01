using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Models;
using FluentAssertions;
using Services.Test.Fakes;
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
            var sut = new ProjectService(repository, null, null, null);
            var expected = testProjects;

            var actual = sut.GetProjects();

            actual.Should().Equal(expected);
        }

        [Fact]
        public void GetOpenProjects_ReturnOnlyOpenProjects()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository, null, null, null);
            var expected = testProjects.Where(p => p.State != ProjectStates.Closed);

            var actual = sut.GetOpenProjects();

            actual.Should().Equal(expected);
        }

        [Fact]
        public void AddNewProject_AddsNewProject()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var newProject = new Project() { ID = 4, State = ProjectStates.Inbox };
            var newProjectPosition = repository.
                                        Projects
                                        .Count(p => p.State == ProjectStates.Inbox) + 1;
            var sut = new ProjectService(repository, null, null, null);

            sut.AddNewProject(newProject);

            repository.Projects.Should().Contain(newProject);

            var newProjectInRepository = repository.Projects.First(p => p.ID == newProject.ID);
            newProjectInRepository.Order.Should().Be(newProjectPosition);
        }

        [Fact]
        public void UpdateProject_UpdatesExistingProject()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository, null, null, null);
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
            var sut = new ProjectService(repository, null, null, null);
            var newProject = new Project() { ID = 0, };

            sut.SaveChanges(newProject);

            repository.Projects.Should().Contain(newProject);
        }

        [Fact]
        public void SaveChanges_ProjectExists_UpdatesExistingProject()
        {
            var testProjects = SetupBasicProjectData();
            var repository = new ProjectRepositoryFake(testProjects);
            var sut = new ProjectService(repository, null, null, null);
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
                new() { ID = 1, Name = "Test1", State = ProjectStates.Inbox },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Closed },
            };
        }
    }
}
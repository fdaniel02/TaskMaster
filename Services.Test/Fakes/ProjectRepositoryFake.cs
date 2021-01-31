using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;

namespace Services.Test.Fakes
{
    public class ProjectRepositoryFake : IProjectRepository
    {
        public ProjectRepositoryFake(List<Project> projects)
        {
            SetupFake(projects);
        }

        public List<Project> Projects { get; set; }

        public IQueryable<Project> GetAll()
        {
            return Projects.AsQueryable();
        }

        public void Add(Project project)
        {
            Projects.Add(project);
        }

        public void Update(Project project)
        {
            var projectToUpdate = Projects.FirstOrDefault(p => p.ID == project.ID);
            var projectToUpdateIndex = Projects.IndexOf(projectToUpdate);
            Projects[projectToUpdateIndex] = project;
        }

        public void SetupFake(List<Project> projects)
        {
            Projects = projects;
        }
    }
}
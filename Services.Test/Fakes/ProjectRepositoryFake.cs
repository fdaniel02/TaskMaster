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
            throw new System.NotImplementedException();
        }

        public void Update(Project project)
        {
            throw new System.NotImplementedException();
        }

        public void SetupFake(List<Project> projects)
        {
            Projects = projects;
        }
    }
}
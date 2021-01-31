using System.Collections.Generic;
using Domain.Models;

namespace TaskMaster.Services
{
    public interface IProjectService
    {
        List<Project> GetProjects();

        List<Project> GetOpenProjects();

        void SaveChanges(Project project);

        void UpdateProject(Project project);

        void AddNewProject(Project project);
    }
}
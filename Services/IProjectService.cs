using System.Collections.Generic;
using Domain.Models;

namespace Services
{
    public interface IProjectService
    {
        List<Project> GetProjects();

        List<Project> GetOpenProjects();

        Project GetProject(int id);

        void SaveChanges(Project project);

        void UpdateProject(Project project);

        void AddNewProject(Project project);

        void AddComment(Project project, string comment);

        void AddActionItem(Project project, string actionItem);

        public void AddTag(Project project, string tag);

        public void RemoveTag(Project project, ProjectTags tag);
    }
}
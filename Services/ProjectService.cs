using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;

namespace Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public List<Project> GetProjects()
        {
            return _projectRepository.GetAll().ToList();
        }

        public List<Project> GetOpenProjects()
        {
            return _projectRepository
                            .GetAll()
                            .Where(p => p.State != ProjectStates.Closed)
                            .ToList();
        }

        public Project GetProject(int id)
        {
            return _projectRepository.GetAll().FirstOrDefault(p => p.ID == id);
        }

        public void SaveChanges(Project project)
        {
            project.LastUpdated = DateTime.Now;

            if (IsNewProject(project.ID))
            {
                AddNewProject(project);
                return;
            }

            UpdateProject(project);
        }

        public void UpdateProject(Project project)
        {
            _projectRepository.Update(project);
        }

        public void AddNewProject(Project project)
        {
            project.Created = DateTime.Now;
            _projectRepository.Add(project);
        }

        public void AddComment(Project project, string comment)
        {
            var newComment = new Comment { Body = comment, Created = DateTime.Now };
            project.Comments.Add(newComment);
        }

        public void AddActionItem(Project project, string actionItem)
        {
            var newActionItem = new ActionItem { Name = actionItem, Finished = false };
            project.ActionItems.Add(newActionItem);

            SaveChanges(project);
        }

        private bool IsNewProject(int projectId)
        {
            return projectId <= 0;
        }
    }
}
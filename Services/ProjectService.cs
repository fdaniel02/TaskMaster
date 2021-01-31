using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;
using Services.Enums;
using TaskMaster.Services;

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
                            .Where(p => p.State.ID != (int)ProjectStates.Closed)
                            .ToList();
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

        private bool IsNewProject(int projectId)
        {
            return projectId <= 0;
        }
    }
}
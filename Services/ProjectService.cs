using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Services.Enums;
using TaskMaster.Domain.Models;
using TaskMaster.Domain.Repositories;

namespace TaskMaster.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<Project>> GetProjects()
        {
            return await _projectRepository.GetAll().ToListAsync();
        }

        public async Task<List<Project>> GetOpenProjects()
        {
            return await _projectRepository
                            .GetAll()
                            .Where(p => p.State.ID != (int)ProjectStates.Closed)
                            .ToListAsync();
        }

        public async void SaveChanges(Project project)
        {
            project.LastUpdated = DateTime.Now;

            if (IsNewProject(project.ID))
            {
                await AddNewProject(project);
                return;
            }

            await UpdateProject(project);
        }

        public async Task UpdateProject(Project project)
        {
            await _projectRepository.Update(project);
        }

        public async Task AddNewProject(Project project)
        {
            project.Created = DateTime.Now;
            await _projectRepository.Add(project);
        }

        private bool IsNewProject(int projectId)
        {
            return projectId <= 0;
        }
    }
}
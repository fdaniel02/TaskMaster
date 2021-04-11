using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;

namespace Services
{
    public class ProjectOrderService : IProjectOrderService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectOrderService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void MoveDown(Project project, int newPosition)
        {
            var projectCount = _projectRepository
                                .GetAll()
                                .Count(p => p.State == project.State);

            if (project.Order > newPosition || projectCount < newPosition)
            {
                throw new ArgumentException("Invalid position!");
            }

            bool Predicate(Project p) => p.Order > project.Order && p.Order <= newPosition;
            Reorder(project, newPosition, Predicate, project.Order);
        }

        public void MoveUp(Project project, int newPosition)
        {
            if (project.Order < newPosition || newPosition <= 0)
            {
                throw new ArgumentException("Invalid position!");
            }

            bool Predicate(Project p) => p.Order >= newPosition && p.Order < project.Order;
            Reorder(project, newPosition, Predicate, newPosition + 1);
        }

        public void RefreshOrder(List<Project> projects, int currentPosition = 1)
        {
            foreach (var project in projects.ToList())
            {
                project.Order = currentPosition;
                currentPosition++;
                _projectRepository.Update(project);
            }
        }

        private void Reorder(Project project, int newPosition, Func<Project, bool> predicate, int position)
        {
            if (project.Order == newPosition)
            {
                return;
            }

            var projects = _projectRepository
                .GetAll()
                .Where(p => p.State == project.State)
                .AsEnumerable()
                .Where(predicate)
                .ToList();

            RefreshOrder(projects, position);

            project.Order = newPosition;
            _projectRepository.Update(project);
        }
    }
}
using System;
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
            Reorder(project, newPosition, Predicate, -1);
        }

        public void MoveUp(Project project, int newPosition)
        {
            if (project.Order < newPosition || newPosition <= 0)
            {
                throw new ArgumentException("Invalid position!");
            }

            bool Predicate(Project p) => p.Order >= newPosition && p.Order < project.Order;
            Reorder(project, newPosition, Predicate, 1);
        }

        private void Reorder(Project project, int newPosition, Func<Project, bool> predicate, int moveValue)
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

            foreach (var proj in projects)
            {
                proj.Order += moveValue;
                _projectRepository.Update(proj);
            }

            project.Order = newPosition;
            _projectRepository.Update(project);
        }
    }
}
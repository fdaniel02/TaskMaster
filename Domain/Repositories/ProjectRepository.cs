using System.Linq;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskMasterContext _context;

        public ProjectRepository(TaskMasterContext context)
        {
            _context = context;
        }

        public IQueryable<Project> GetAll()
        {
            return _context.Projects
                .Include(p => p.State)
                .Include(p => p.ActionItems)
                .AsQueryable();
        }

        public void Add(Project project)
        {
            _context.Projects.AddAsync(project);
            _context.SaveChanges();
        }

        public void Update(Project project)
        {
            _context.Projects.Attach(project);
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
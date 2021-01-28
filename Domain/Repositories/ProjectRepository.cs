using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Domain.Models;

namespace TaskMaster.Domain.Repositories
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

        public async Task Add(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Project project)
        {
            _context.Projects.Attach(project);
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
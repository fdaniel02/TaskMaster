using System.Linq;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public ProjectRepository(TaskMasterContext context)
        {
            Context = context;
        }

        public TaskMasterContext Context { get; }

        public IQueryable<Project> GetAll()
        {
            return Context.Projects
                .Include(p => p.ActionItems)
                .Include(p => p.Comments)
                .Include(p => p.ProjectTags)
                .ThenInclude(p => p.Tag)
                .AsQueryable();
        }

        public void Add(Project project)
        {
            Context.Projects.AddAsync(project);
            Context.SaveChanges();
        }

        public void Update(Project project)
        {
            Context.Projects.Attach(project);
            Context.Entry(project).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
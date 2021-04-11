using System.Linq;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IProjectRepository
    {
        TaskMasterContext Context { get; }

        IQueryable<Project> GetAll();

        void Add(Project project);

        void Update(Project project);
    }
}
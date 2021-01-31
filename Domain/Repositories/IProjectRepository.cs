using System.Linq;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IProjectRepository
    {
        IQueryable<Project> GetAll();

        void Add(Project project);

        void Update(Project project);
    }
}
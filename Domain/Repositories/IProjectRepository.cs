using System.Linq;
using System.Threading.Tasks;
using TaskMaster.Domain.Models;

namespace TaskMaster.Domain.Repositories
{
    public interface IProjectRepository
    {
        IQueryable<Project> GetAll();

        Task Add(Project project);

        Task Update(Project project);
    }
}
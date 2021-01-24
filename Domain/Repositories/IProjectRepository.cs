using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMaster.Domain.Models;

namespace TaskMaster.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAll();

        Task Add(Project project);

        Task Update(Project project);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMaster.Domain.Models;

namespace TaskMaster.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjects();

        Task<List<Project>> GetOpenProjects();

        void SaveChanges(Project project);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMaster.Domain.Models;

namespace TaskMaster.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjects();

        void SaveChanges(Project project);
    }
}
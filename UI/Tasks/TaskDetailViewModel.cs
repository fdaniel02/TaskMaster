using System.Linq;
using TaskMaster.Domain.Models;
using TaskMaster.Services;

namespace UI.Tasks
{
    public class TaskDetailViewModel : BindableBase
    {
        private readonly IProjectService _projectService;
        private Project _project;

        public TaskDetailViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public Project Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        public async void Load()
        {
            var projects = await _projectService.GetProjects();
            Project = projects.LastOrDefault();
        }
    }
}
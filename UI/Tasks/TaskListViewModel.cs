using System.Collections.ObjectModel;
using TaskMaster.Domain.Models;
using TaskMaster.Services;

namespace UI.Tasks
{
    public class TaskListViewModel : BindableBase
    {
        private readonly IProjectService _projectService;
        private ObservableCollection<Project> _projects;

        public TaskListViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }

        public async void Load()
        {
            var projects = await _projectService.GetProjects();
            Projects = new ObservableCollection<Project>(projects);
        }
    }
}
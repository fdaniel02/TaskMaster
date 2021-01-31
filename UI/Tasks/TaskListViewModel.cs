using System.Collections.ObjectModel;
using Domain.Models;
using Prism.Events;
using TaskMaster.Services;
using UI.Tasks.Events;

namespace UI.Tasks
{
    public class TaskListViewModel : BindableBase
    {
        private readonly IProjectService _projectService;

        private readonly IEventAggregator _eventAggregator;

        private ObservableCollection<Project> _projects;

        private Project _selectedProject;

        public TaskListViewModel(IProjectService projectService, IEventAggregator eventAggregator)
        {
            _projectService = projectService;
            _eventAggregator = eventAggregator;
        }

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }

        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                SetProperty(ref _selectedProject, value);
                _eventAggregator.GetEvent<ProjectSelectedEvent>().Publish(_selectedProject);
            }
        }

        public void Load()
        {
            var projects = _projectService.GetOpenProjects();
            Projects = new ObservableCollection<Project>(projects);
        }
    }
}
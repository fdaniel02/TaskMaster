using System.Collections.ObjectModel;
using System.Linq;
using Domain.Models;
using Prism.Commands;
using Prism.Events;
using Services;
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

            eventAggregator.GetEvent<UpdateProjectListEvent>().Subscribe(Load);

            AddProjectCommand = new DelegateCommand(AddProject);
        }

        public DelegateCommand AddProjectCommand { get; }

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
            var projects = _projectService.GetOpenProjects().OrderBy(p => p.State);
            Projects = new ObservableCollection<Project>(projects);
        }

        private void AddProject()
        {
            SelectedProject = new Project();
        }
    }
}
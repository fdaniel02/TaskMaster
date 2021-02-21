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

        private readonly DelegateCommand _addProjectCommand;

        private ObservableCollection<Project> _projects;

        private Project _selectedProject;

        private bool _showClosedProjects = false;

        public TaskListViewModel(IProjectService projectService, IEventAggregator eventAggregator)
        {
            _projectService = projectService;
            _eventAggregator = eventAggregator;

            eventAggregator.GetEvent<UpdateProjectListEvent>().Subscribe(Load);

            _addProjectCommand = new DelegateCommand(AddProject);
        }

        public DelegateCommand AddProjectCommand => _addProjectCommand;

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

        public bool ShowClosedProjects
        {
            get => _showClosedProjects;
            set => SetProperty(ref _showClosedProjects, value);
        }

        public void Load()
        {
            var selectedProject = _selectedProject;

            var projects = _projectService
                .GetProjects()
                .OrderBy(p => p.State)
                .ThenBy(p => p.Priority);

            Projects = new ObservableCollection<Project>(projects);

            SelectedProject = selectedProject;
        }

        private void AddProject()
        {
            SelectedProject = new Project();
        }
    }
}
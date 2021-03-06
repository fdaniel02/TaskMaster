using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Domain.Enums;
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

        private bool _showClosedProjects = false;

        public TaskListViewModel(IProjectService projectService, IEventAggregator eventAggregator)
        {
            _projectService = projectService;
            _eventAggregator = eventAggregator;

            eventAggregator.GetEvent<UpdateProjectListEvent>().Subscribe(Load);

            AddProjectCommand = new DelegateCommand(AddProject);
        }

        public DelegateCommand AddProjectCommand { get; }

        public ICollectionView ProjectView { get; private set; }

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
            set
            {
                SetProperty(ref _showClosedProjects, value);
                ProjectView.Refresh();
            }
        }

        public void Load()
        {
            var selectedProject = _selectedProject;

            var projects = _projectService
                .GetProjects()
                .OrderBy(p => p.State)
                .ThenBy(p => p.Priority);

            Projects = new ObservableCollection<Project>(projects);

            // The first project is selected by default
            SelectedProject = selectedProject ?? projects.FirstOrDefault();

            ProjectView = CollectionViewSource.GetDefaultView(Projects);

            ProjectView.Filter = ProjectFilter;
            ProjectView.GroupDescriptions.Add(new PropertyGroupDescription("State"));
            OnPropertyChanged(nameof(ProjectView));
        }

        private bool ProjectFilter(object p)
        {
            if (p is not Project project)
            {
                return false;
            }

            return ShowClosedProjects || project.State != ProjectStates.Closed;
        }

        private void AddProject()
        {
            SelectedProject = new Project();
        }
    }
}
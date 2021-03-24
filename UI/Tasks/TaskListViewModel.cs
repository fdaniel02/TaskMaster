using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Domain.Models;
using Prism.Commands;
using Prism.Events;
using Services;
using UI.Tasks.Events;
using UI.Tasks.Filters;

namespace UI.Tasks
{
    public class TaskListViewModel : BindableBase
    {
        private readonly IProjectService _projectService;

        private readonly IEventAggregator _eventAggregator;

        private readonly IProjectFilter _projectFilter;

        private ObservableCollection<Project> _projects;

        private Project _selectedProject;

        private bool _showClosedProjects = false;

        private string _searchExpression;

        public TaskListViewModel(
            IProjectService projectService,
            IEventAggregator eventAggregator,
            IProjectFilter projectFilter)
        {
            _projectService = projectService;
            _eventAggregator = eventAggregator;
            _projectFilter = projectFilter;

            eventAggregator.GetEvent<UpdateProjectListEvent>().Subscribe(Load);

            AddProjectCommand = new DelegateCommand(AddProject);
            SearchCommand = new DelegateCommand(SearchProject);
            ClearSearchCommand = new DelegateCommand(ClearSearch);
        }

        public DelegateCommand AddProjectCommand { get; }

        public DelegateCommand SearchCommand { get; }

        public DelegateCommand ClearSearchCommand { get; }

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

        public string SearchExpression
        {
            get => _searchExpression;
            set
            {
                SetProperty(ref _searchExpression, value);
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

            return _projectFilter.FilterProject(project, SearchExpression, ShowClosedProjects);
        }

        private void AddProject()
        {
            SelectedProject = new Project();
        }

        private void SearchProject()
        {
            ProjectView.Refresh();
        }

        private void ClearSearch()
        {
            SearchExpression = string.Empty;
            SearchProject();
        }
    }
}
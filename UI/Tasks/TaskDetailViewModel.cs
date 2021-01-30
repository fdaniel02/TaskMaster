using Prism.Events;
using TaskMaster.Domain.Models;
using TaskMaster.Services;
using UI.Tasks.Events;

namespace UI.Tasks
{
    public class TaskDetailViewModel : BindableBase
    {
        private readonly IProjectService _projectService;
        private readonly IEventAggregator _eventAggregator;
        private Project _project;

        public TaskDetailViewModel(IProjectService projectService, IEventAggregator eventAggregator)
        {
            _projectService = projectService;
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<ProjectSelectedEvent>().Subscribe(Load);
        }

        public Project Project
        {
            get => _project;
            set
            {
                SetProperty(ref _project, value);
                OnPropertyChanged(nameof(ShowDetails));
            }
        }

        public bool ShowDetails => Project is not null;

        public void Load(Project project)
        {
            Project = project;
        }
    }
}
using Domain.Models;
using Prism.Commands;
using Prism.Events;
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

            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        public DelegateCommand SaveCommand { get; }

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

        private void Save()
        {
            _projectService.SaveChanges(Project);
        }

        private bool CanSave()
        {
            return true;
        }
    }
}
using System.Collections.ObjectModel;
using Domain.Models;
using Prism.Commands;
using Prism.Events;
using Services;
using UI.Tasks.Events;

namespace UI.Tasks
{
    public class TaskDetailViewModel : BindableBase
    {
        private readonly IProjectService _projectService;

        private readonly IEventAggregator _eventAggregator;

        private Project _project;

        private string _comment;

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
                OnPropertyChanged(nameof(Comments));
            }
        }

        public ObservableCollection<Comment> Comments
            => new(Project?.Comments);

        public string Comment
        {
            get => _comment;
            set
            {
                SetProperty(ref _comment, value);
            }
        }

        public bool ShowDetails => Project is not null;

        public void Load(Project project)
        {
            Project = project;
            Comment = string.Empty;
        }

        private void Save()
        {
            AddComment();

            _projectService.SaveChanges(Project);
            _eventAggregator.GetEvent<UpdateProjectListEvent>().Publish();

            Load(Project);
        }

        private bool CanSave()
        {
            return true;
        }

        private void AddComment()
        {
            if (string.IsNullOrEmpty(Comment))
            {
                return;
            }

            _projectService.AddComment(Project, Comment);
        }
    }
}
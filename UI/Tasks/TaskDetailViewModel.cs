using System;
using System.Collections.ObjectModel;
using System.Linq;
using Domain.Models;
using Prism.Commands;
using Prism.Events;
using Services;
using UI.Notifications;
using UI.Tasks.Constants;
using UI.Tasks.Events;

namespace UI.Tasks
{
    public class TaskDetailViewModel : BindableBase
    {
        private readonly IProjectService _projectService;

        private readonly IEventAggregator _eventAggregator;

        private readonly INotificationService _notificationService;

        private Project _project;

        private string _comment;

        private string _actionItem;

        public TaskDetailViewModel(
            IProjectService projectService,
            IEventAggregator eventAggregator,
            INotificationService notificationService)
        {
            _projectService = projectService;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;

            eventAggregator.GetEvent<ProjectSelectedEvent>().Subscribe(Load);

            SaveCommand = new DelegateCommand(SaveChanges, CanSave);
            AddActionItemCommand = new DelegateCommand(AddActionItem, CanAddActionItem);
            ToogleActionItemCommand = new DelegateCommand<ActionItem>(ToogleActionItem);
        }

        public DelegateCommand SaveCommand { get; }

        public DelegateCommand AddActionItemCommand { get; }

        public DelegateCommand<ActionItem> ToogleActionItemCommand { get; }

        public Project Project
        {
            get => _project;
            set
            {
                SetProperty(ref _project, value);
                OnPropertyChanged(nameof(ShowDetails));
                OnPropertyChanged(nameof(Comments));
                OnPropertyChanged(nameof(OpenActionItems));
                OnPropertyChanged(nameof(ClosedActionItems));
            }
        }

        public ObservableCollection<Comment> Comments
            => Project is null
                ? new()
                : new(Project.Comments);

        public ObservableCollection<ActionItem> OpenActionItems
            => Project is null
                ? new()
                : new(Project.ActionItems.Where(a => !a.Finished));

        public ObservableCollection<ActionItem> ClosedActionItems
            => Project is null
                ? new()
                : new(Project.ActionItems.Where(a => a.Finished));

        public string ActionItem
        {
            get => _actionItem;
            set
            {
                SetProperty(ref _actionItem, value);
                AddActionItemCommand.RaiseCanExecuteChanged();
            }
        }

        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        public bool ShowDetails => Project is not null;

        public void Load(Project project)
        {
            Project = project;
            Comment = string.Empty;
            ActionItem = string.Empty;
        }

        private void SaveChanges()
        {
            AddComment();

            _projectService.SaveChanges(Project);
            _eventAggregator.GetEvent<UpdateProjectListEvent>().Publish();

            _notificationService.ShowSuccessMessage(NotificationMessages.SaveSuccess);

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

        // TODO: move the command handlers to interfaces and use DI. Thus making unit testing easier
        private void AddActionItem()
        {
            try
            {
                _projectService.AddActionItem(Project, ActionItem);
            }
            catch (ArgumentNullException)
            {
                _notificationService.ShowErrorMessage(NotificationMessages.ActionItemSaveErrorMissingProject);
            }
            catch (Exception)
            {
                _notificationService.ShowErrorMessage(NotificationMessages.ActionItemSaveErrorUnknown);
            }

            Load(Project);
        }

        private bool CanAddActionItem()
        {
            return !string.IsNullOrEmpty(ActionItem);
        }

        private void ToogleActionItem(ActionItem actionItem)
        {
            Project.ActionItems.FirstOrDefault(a => a.ID == actionItem.ID).Finished = !actionItem.Finished;

            _projectService.SaveChanges(Project);
            Load(Project);
        }
    }
}
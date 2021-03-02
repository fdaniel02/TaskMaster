using System.Threading.Tasks;
using Prism.Events;
using UI.Notifications.Events;
using UI.Notifications.Models;
using UI.Tasks;

namespace UI
{
    public class MainWindowViewModel : BindableBase
    {
        private const int DefaultNotificationTimeout = 3000;

        private readonly IEventAggregator _eventAggregator;

        private TaskListViewModel _taskListViewModel;

        private TaskDetailViewModel _taskDetailViewModel;

        private NotificationModel _notification;

        private bool _showNotification;

        public MainWindowViewModel(
            TaskListViewModel taskListViewModel,
            TaskDetailViewModel taskDetailViewModel,
            IEventAggregator eventAggregator)
        {
            TaskListViewModel = taskListViewModel;
            TaskDetailViewModel = taskDetailViewModel;

            eventAggregator.GetEvent<ShowNotificationEvent>().Subscribe(ShowNotificationMessage);
            _eventAggregator = eventAggregator;
        }

        public TaskListViewModel TaskListViewModel
        {
            get => _taskListViewModel;
            set => SetProperty(ref _taskListViewModel, value);
        }

        public TaskDetailViewModel TaskDetailViewModel
        {
            get => _taskDetailViewModel;
            set => SetProperty(ref _taskDetailViewModel, value);
        }

        public NotificationModel Notification
        {
            get => _notification;
            set => SetProperty(ref _notification, value);
        }

        public bool ShowNotification
        {
            get => _showNotification;
            set => SetProperty(ref _showNotification, value);
        }

        private void ShowNotificationMessage(NotificationModel notificationModel)
        {
            Notification = notificationModel;
            ShowNotification = true;

            Task.Delay(DefaultNotificationTimeout).ContinueWith((_) => ShowNotification = false);
        }
    }
}
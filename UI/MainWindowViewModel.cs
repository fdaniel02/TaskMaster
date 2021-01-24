using UI.Tasks;

namespace UI
{
    public class MainWindowViewModel : BindableBase
    {
        private TaskListViewModel _taskListViewModel;
        private TaskDetailViewModel _taskDetailViewModel;

        public MainWindowViewModel(TaskListViewModel taskListViewModel, TaskDetailViewModel taskDetailViewModel)
        {
            TaskListViewModel = taskListViewModel;
            TaskDetailViewModel = taskDetailViewModel;
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
    }
}
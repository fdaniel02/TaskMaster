using MahApps.Metro.Controls;
using UI.Tasks;

namespace UI
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(TaskListViewModel taskListViewModel, TaskDetailViewModel taskDetailViewModel)
        {
            DataContext = new MainWindowViewModel(taskListViewModel, taskDetailViewModel);
            InitializeComponent();
        }
    }
}
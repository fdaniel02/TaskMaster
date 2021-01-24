using System.Windows;
using UI.Tasks;

namespace UI
{
    public partial class MainWindow : Window
    {
        public MainWindow(TaskListViewModel taskListViewModel, TaskDetailViewModel taskDetailViewModel)
        {
            DataContext = new MainWindowViewModel(taskListViewModel, taskDetailViewModel);
            InitializeComponent();
        }
    }
}
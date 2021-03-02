using MahApps.Metro.Controls;
using Prism.Events;
using UI.Tasks;

namespace UI
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(
            TaskListViewModel taskListViewModel,
            TaskDetailViewModel taskDetailViewModel,
            IEventAggregator eventAggregator)
        {
            DataContext = new MainWindowViewModel(taskListViewModel, taskDetailViewModel, eventAggregator);
            InitializeComponent();
        }
    }
}
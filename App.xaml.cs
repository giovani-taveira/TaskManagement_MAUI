using TaskManagement.MVVM.Views;
using TaskManagement.MVVM.Views.MainTask;
using TaskManagement.Services.Interfaces;

namespace TaskManagement
{
    public partial class App : Application
    {
        public App(IMainTaskService mainTaskService, ISubTaskService subTaskService)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainTasksPage(mainTaskService, subTaskService));
        }
    }
}

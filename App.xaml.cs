using TaskManagement.MVVM.Views;
using TaskManagement.MVVM.Views.MainTask;
using TaskManagement.Services.Interfaces;

namespace TaskManagement
{
    public partial class App : Application
    {
        private readonly IMainTaskService _mainTaskService;
        public App(IMainTaskService mainTaskService)
        {
            InitializeComponent();

            _mainTaskService = mainTaskService;

            MainPage = new NavigationPage(new MainTasksPage(_mainTaskService));
        }
    }
}

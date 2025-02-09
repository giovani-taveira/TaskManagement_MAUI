using TaskManagement.Services.Interfaces;

namespace TaskManagement
{
    public partial class App : Application
    {
        public App(IMainTaskService mainTaskService, ISubTaskService subTaskService)
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}

using TaskManagement.MVVM.Views.MainTask;
using TaskManagement.MVVM.Views.SubTask;

namespace TaskManagement
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("mainTask", typeof(MainTasksPage));
            Routing.RegisterRoute("mainTask/addEditMainTask", typeof(AddEditMainTask));
            Routing.RegisterRoute("mainTask/subTasks", typeof(SubTasksPage));
            Routing.RegisterRoute("mainTask/subTasks/addEditSubTask", typeof(AddEditSubTask));
        }
    }
}

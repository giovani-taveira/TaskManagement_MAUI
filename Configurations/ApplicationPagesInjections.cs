using TaskManagement.MVVM.Views.MainTask;
using TaskManagement.MVVM.Views.SubTask;

namespace TaskManagement.Configurations
{
    public static class ApplicationPagesInjections
    {
        public static void AddApplicationPagesInjections(this IServiceCollection services)
        {
            services.AddSingleton<AppShell>();
            services.AddTransient<MainPage>();
            services.AddTransient<MainTasksPage>();
            services.AddTransient<AddEditMainTask>();
            services.AddTransient<SubTasksPage>();
            services.AddTransient<AddEditSubTask>();
        }
    }
}

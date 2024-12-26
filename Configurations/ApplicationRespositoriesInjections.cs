using TaskManagement.MVVM.Models;
using TaskManagement.Persistence.Respositories;

namespace TaskManagement.Configurations
{
    public static class ApplicationRespositoriesInjections
    {
        public static void AddApplicationRepositoriesInjections(this IServiceCollection services)
        {
            services.AddScoped<IRepository<MainTask>, Repository<MainTask>>();
            services.AddScoped<IRepository<SubTask>, Repository<SubTask>>();
        }
    }
}

using TaskManagement.Services;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Configurations
{
    public static class ApplicationServicesInjections
    {
        public static void AddApplicationServicesInjections(this IServiceCollection services)
        {
            services.AddScoped<IMainTaskService, MainTaskService>();
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.ViewModels.MainTasks
{
    partial class MainTaskDetailsViewModel : ObservableObject
    {
        private readonly IMainTaskService _mainTaskService;
        private readonly Guid _taskId;

        public MainTaskDetailsViewModel(
            IMainTaskService mainTaskService,
            Guid taskId)
        {
            _mainTaskService = mainTaskService;
            _taskId = taskId;
        }
    
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _description;


        [RelayCommand]
        public async Task GetMainTaskById()
        {
            var task = await _mainTaskService.GetMainTasksById(_taskId);

            if (task != null)
            {
                Title = task.Title;
                Description = task.Description;
            }
        }
    }
}

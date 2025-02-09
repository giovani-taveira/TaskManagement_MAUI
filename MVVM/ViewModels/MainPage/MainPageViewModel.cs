using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManagement.Helpers.Enums;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.ViewModels.MainPage
{
    partial class MainPageViewModel : ObservableObject
    {
        private readonly IMainTaskService _mainTaskService;


        public MainPageViewModel(IMainTaskService mainTaskService)
        {
            _mainTaskService = mainTaskService;
        }


        [ObservableProperty]
        private int _activeMainTasksCount;

        [ObservableProperty]
        private int _activeListTaskCount;

        [ObservableProperty]
        private bool _isMainTaskCounterEnabled = true;

        [ObservableProperty]
        private bool _isListTaskCounterEnabled = true;


        [RelayCommand]
        public async Task GetAllTasks()
        {
            var mainTasks = await _mainTaskService.GetAllMainTasks();
            var activeMainTasks = mainTasks.Where(x => x.Status == StatusEnum.Ativo.ToString() || x.Status == StatusEnum.Em_Atraso.ToString().Replace('_', ' '));

            if (activeMainTasks.Any())
            {
                IsMainTaskCounterEnabled = true;
                ActiveMainTasksCount = activeMainTasks.Count();
            }          
            else
                IsMainTaskCounterEnabled = false;

        }
    }
}

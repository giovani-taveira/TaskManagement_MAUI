using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TaskManagement.DTOs.MainTask;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.ViewModels
{
    public partial class MainTaskViewModel : ObservableObject
    {
        private readonly IMainTaskService _mainTaskService;

        public MainTaskViewModel(IMainTaskService mainTaskService)
        {
            _mainTaskService = mainTaskService;
        }

        [ObservableProperty]
        public bool _isLoading;

        [ObservableProperty]
        public bool _isNotLoading;

        public ObservableCollection<MainTaskDTO> MainTasks { get; set; } = new();

        private List<MainTaskDTO> AllMainTasks { get; set; } = new List<MainTaskDTO>();


        [RelayCommand]
        public async Task GetAllMainTasks()
        {         
            MainTasks.Clear();
            AllMainTasks.Clear();

            IsLoading = true;
            IsNotLoading = false;

            var tasks = await _mainTaskService.GetAllMainTasks();
            AllMainTasks.AddRange(tasks);
            FillProgressDrawable(tasks);

            IsLoading = !IsLoading;
            IsNotLoading = !IsNotLoading;
        }

        [RelayCommand]
        public async Task GetMainTasksById(int id)
        {
            MainTasks.Clear();
            AllMainTasks.Clear();

            IsLoading = true;
            IsNotLoading = false;

            var tasks = await _mainTaskService.GetAllMainTasks();
            AllMainTasks.AddRange(tasks);
            FillProgressDrawable(tasks);

            IsLoading = !IsLoading;
            IsNotLoading = !IsNotLoading;
        }

        [RelayCommand]
        public void SearchMainTask(string searchValue)
        {
            var tasks = AllMainTasks.Where(x => (x.Title.Equals(string.Empty) || x.Title.Contains(searchValue))).ToList();           
            FillProgressDrawable(tasks);       
        }

        public void SearchAllMainTasks()
        {
            var tasks = AllMainTasks;       
            FillProgressDrawable(tasks);
        }

        public void SearchAllMainTasksByStatus(string status)
        {
            var tasks = status.Equals("Todos") ? AllMainTasks : AllMainTasks.Where(x => x.Status.Equals(status)).ToList();        
            FillProgressDrawable(tasks);
        }

        private void FillProgressDrawable(List<MainTaskDTO> tasks)
        {
            MainTasks.Clear();

            foreach (var task in tasks)
            {
                task.CircularProgressDrawableInstance.Progress = task.ProgressDrawable;
                task.CircularProgressDrawableInstance.Text = task.QtdSubTasks;

                MainTasks.Add(task);
            }
        }
    }
}

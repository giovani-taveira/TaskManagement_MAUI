using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TaskManagement.DTOs.SubTask;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.ViewModels.SubTasks
{
    public partial class SubTaskViewModel : ObservableObject
    {
        private readonly ISubTaskService _subTaskService;
        private Guid _mainTaskId;

        public SubTaskViewModel(ISubTaskService subTaskService, Guid mainTaskId)
        {
            _subTaskService = subTaskService;
            _mainTaskId = mainTaskId;
        }

        [ObservableProperty]
        public bool _isLoading;

        [ObservableProperty]
        public bool _isNotLoading;

        public ObservableCollection<SubTaskDTO> SubTasks { get; set; } = new();

        private List<SubTaskDTO> AllSubTasks { get; set; } = new List<SubTaskDTO>();


        [RelayCommand]
        public async Task GetAllSubTasks()
        {                   
            IsLoading = true;
            IsNotLoading = false;

            var tasks = await _subTaskService.GetAllSubTasks(_mainTaskId);

            SubTasks.Clear();
            tasks.ForEach(SubTasks.Add);

            AllSubTasks.Clear();
            AllSubTasks.AddRange(tasks);

            IsLoading = false;
            IsNotLoading = true;
        }

        [RelayCommand]
        public void SearchSubTasks(string searchValue)
        {
            var tasks = AllSubTasks.Where(x => (x.Title.Equals(string.Empty) || x.Title.Contains(searchValue))).ToList();
            SubTasks.Clear();
            tasks.ForEach(SubTasks.Add);
        }

        public void SearchAllMainTasks()
        {
            var tasks = AllSubTasks;
            SubTasks.Clear();
            tasks.ForEach(SubTasks.Add);
        }

        public void SearchAllMainTasksByStatus(string status)
        {
            var tasks = status.Equals("Todos") ? AllSubTasks : AllSubTasks.Where(x => x.Status.Equals(status)).ToList();
            SubTasks.Clear();
            tasks.ForEach(SubTasks.Add);
        }
    }
}

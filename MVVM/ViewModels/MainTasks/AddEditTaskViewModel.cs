using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using TaskManagement.DTOs.MainTask;
using TaskManagement.Helpers.Enums;
using TaskManagement.MVVM.Views._Components;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.ViewModels.MainTasks
{
    partial class AddEditTaskViewModel : ObservableValidator
    {
        private readonly IMainTaskService _mainTaskService;
        private readonly INavigation _navigation;
        public AddEditTaskViewModel(IMainTaskService mainTaskService, 
            INavigation navigation,
            Guid? mainTaskId)
        {
            _mainTaskService = mainTaskService;
            _navigation = navigation;
            Id = mainTaskId;
        }

        [ObservableProperty]
        private Guid? _id;

        [ObservableProperty]
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 100 caracteres!")]
        private string _title;

        [ObservableProperty]
        [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 500 caracteres!")]
        private string _description;

        [ObservableProperty]
        private DateTime? _deadlineDate;

        [ObservableProperty]
        private string _status;

        [ObservableProperty]
        private bool _isNotifiable;


        [RelayCommand]
        public async Task GetMainTaskById()
        {
            if (Id == null)
            {
                DeadlineDate = DateTime.Now;
                return;
            }

            var task = await _mainTaskService.GetMainTasksById(Id.Value);

            if (task != null)
            {
                Title = task.Title;
                Description = task.Description;
                DeadlineDate = task.DeadlineDate ?? DateTime.Now;
                IsNotifiable = task.IsNotifiable;
            }
            else
            {
                DeadlineDate = DateTime.Now;
            }
        }


        [RelayCommand]
        private async Task AddMainTaskAsync()
        {
            this.ValidateAllProperties(); 
            var errors = this.GetErrors();

            if (errors.Any())
            {
                await Toast.Make(errors.First().ErrorMessage, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                return;
            }

            if (Id.HasValue)
                await UpdateMainTask();
            else
                await CreateMainTask();

        }   
        
        private async Task CreateMainTask()
        {
            var mainTask = new AddEditMainTaskDTO
            (
                Id: null,
                Title: Title,
                Description: Description,
                DeadlineDate: DeadlineDate,
                Status: StatusEnum.Ativo.ToString(),
                IsNotifiable: IsNotifiable
            );

            var response = await _mainTaskService.CreateMainTask(mainTask);

            if (response.Sucess)
            {
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Tarefa Criada com Sucesso!", 3000));
                await _navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", response.Message, 5000));
            }
        }

        private async Task UpdateMainTask()
        {
            var mainTask = new AddEditMainTaskDTO
            (
                Id: Id,
                Title: Title,
                Description: Description,
                DeadlineDate: DeadlineDate,
                Status: StatusEnum.Ativo.ToString(),
                IsNotifiable: IsNotifiable
            );

            var response = await _mainTaskService.UpdateMainTask(mainTask);

            if (response.Sucess)
            {
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Tarefa Atualizada com Sucesso!", 3000));
                await _navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", response.Message, 5000));
            }
        }
    }
}



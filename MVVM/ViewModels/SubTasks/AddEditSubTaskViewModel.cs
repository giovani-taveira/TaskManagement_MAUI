using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using TaskManagement.DTOs.SubTask;
using TaskManagement.Helpers.Enums;
using TaskManagement.MVVM.Views._Components;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.ViewModels.SubTasks
{
    partial class AddEditSubTaskViewModel : ObservableValidator
    {
        private readonly ISubTaskService _subTaskService;
        private readonly INavigation _navigation;
        public AddEditSubTaskViewModel(ISubTaskService subTaskService,
            INavigation navigation,
            Guid? subTaskTaskId,
            Guid mainTaskId)
        {
            _subTaskService = subTaskService;
            _navigation = navigation;
            Id = subTaskTaskId;
            MainTaskId = mainTaskId;
        }

        [ObservableProperty]
        private Guid? _id;

        [ObservableProperty]
        private Guid _mainTaskId;

        [ObservableProperty]
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 100 caracteres!")]
        private string _title;

        [ObservableProperty]
        [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 500 caracteres!")]
        private string _description;

        [ObservableProperty]
        private string _status;

        [ObservableProperty]
        private DateTime? _deadlineDate = null;

        [ObservableProperty]
        private string _pageTitle;


        [RelayCommand]
        public async Task GetSubTaskById()
        {
            if (Id == null)
            {
                PageTitle = "Criar Sub Tarefa";
                DeadlineDate = DateTime.Now;
                return;
            }

            var task = await _subTaskService.GetSubTaskById(Id.Value);

            if (task != null)
            {
                PageTitle = "Editar Sub Tarefa";
                Title = task.Title;
                Description = task.Description;
                DeadlineDate = task.DeadlineDate ?? DateTime.Now; ;
            }
        }


        [RelayCommand]
        public async Task SaveSubTaskAsync()
        {
            this.ValidateAllProperties();
            var errors = this.GetErrors();

            if (errors.Any())
            {
                await Toast.Make(errors.First().ErrorMessage, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                return;
            }

            if (Id.HasValue)
                await UpdateSubTask();
            else
                await CreateSubTask();
        }

        private async Task CreateSubTask()
        {
            var subTask = new AddEditSubTaskDTO
            (
                Id: null,
                MainTaskId: MainTaskId,
                Title: Title,
                Description: Description,
                Status: StatusEnum.Ativo.ToString(),
                DeadlineDate: DeadlineDate
            );

            var response = await _subTaskService.CreateSubTask(subTask);

            if (response.Sucess)
            {
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Sub tarefa criada com sucesso!", 3000));
                await _navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", response.Message, 5000));
            }
        }

        private async Task UpdateSubTask()
        {
            var subTask = new AddEditSubTaskDTO
            (
                Id: Id,
                MainTaskId: MainTaskId,
                Title: Title,
                Description: Description,
                Status: StatusEnum.Ativo.ToString(),
                DeadlineDate: DeadlineDate
            );

            var response = await _subTaskService.UpdateSubTask(subTask);

            if (response.Sucess)
            {
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Sub tarefa atualizada com sucesso!", 3000));
                await _navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", response.Message, 5000));
            }
        }
    }
}

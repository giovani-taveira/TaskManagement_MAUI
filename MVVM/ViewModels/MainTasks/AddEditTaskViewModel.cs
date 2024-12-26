using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Helpers.CustomDataAnnotations;
using TaskManagement.Helpers.Enums;
using TaskManagement.MVVM.Models;
using TaskManagement.MVVM.Views._Components;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.ViewModels.MainTasks
{
    partial class AddEditTaskViewModel : ObservableValidator
    {
        private readonly IMainTaskService _mainTaskService;
        private readonly INavigation _navigation;
        public AddEditTaskViewModel(IMainTaskService mainTaskService, 
            INavigation navigation)
        {
            _mainTaskService = mainTaskService;
            _navigation = navigation;
        }

        [ObservableProperty]
        private Guid _id;

        [ObservableProperty]
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 100 caracteres!")]
        private string _title;

        [ObservableProperty]
        [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 500 caracteres!")]
        private string _description;

        [ObservableProperty]
        [DateTimeGreaterThanOrEqualToday(ErrorMessage = "A data de previsão não pode ser anterior ao dia de hoje!")]
        private DateTime? _deadlineDate = DateTime.Now;

        [ObservableProperty]
        private string _status;

        [ObservableProperty]
        private bool _isNotifiable;

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

            var mainTask = new MainTask
            {
                Title = Title,
                Description = Description,
                DeadlineDate = DeadlineDate,
                Status = StatusEnum.Ativo.ToString(),
                IsNotifiable = IsNotifiable
            };

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
    }
}



using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagement.Helpers.Enums;
using TaskManagement.MVVM.Views._Components;
using TaskManagement.Services.Interfaces;
using static TaskManagement.Helpers.Messages.SubTaskMessages;

namespace TaskManagement.MVVM.ViewModels.SubTasks
{
    public partial class SubTaskDetailsViewModel : ObservableObject
    {
        private readonly ISubTaskService _subTaskService;
        private readonly Guid _taskId;

        public SubTaskDetailsViewModel(
            ISubTaskService subTaskService,
            Guid taskId)
        {
            _subTaskService = subTaskService;
            _taskId = taskId;
        }

        public event Action CloseBottomSheetRequested;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _createdAt;

        [ObservableProperty]
        private string _concludedAt;

        [ObservableProperty]
        private bool _isCompleteButtonEnabled = true;

        [ObservableProperty]
        private bool _isReactivateButtonEnabled = false;


        [RelayCommand]
        public async Task GetSubTaskById()
        {
            var task = await _subTaskService.GetSubTaskById(_taskId);

            if (task != null)
            {
                Title = task.Title;
                Description = task.Description;
                CreatedAt = task.CreatedAt?.ToString("dd/MM/yy");
                ConcludedAt = task.ConcludedAt?.ToString("dd/MM/yy");

                if (task.Status == StatusEnum.Concluido.ToString())
                {
                    IsCompleteButtonEnabled = false;
                    IsReactivateButtonEnabled = true;
                }
            }
        }

        [RelayCommand]
        public async Task DeleteSubTask()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Atenção", "Tem certeza que deseja deletar a sub tarefa? Essa ação não poderá ser desfeita!", "Sim", "Não");
            if (!result) return;

            var deleteResponse = await _subTaskService.DeleteSubTask(_taskId);

            if (deleteResponse.Sucess)
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Sub tarefa deletada com sucesso!", 3000));
            else
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", deleteResponse.Message, 5000));

            WeakReferenceMessenger.Default.Send(new SubTaskBottomSheetClosedMessage("Close BottomSheet"));
        }

        [RelayCommand]
        public async Task CompleteSubTask()
        {
            var updateResponse = await _subTaskService.CompleteSubTask(_taskId);

            if (updateResponse.Sucess)
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Sub tarefa concluída com sucesso!", 3000));
            else
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", updateResponse.Message, 5000));

            WeakReferenceMessenger.Default.Send(new SubTaskBottomSheetClosedMessage("Close BottomSheet"));
        }

        [RelayCommand]
        public async Task ReactivateSubTask()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Atenção", "Deseja reativar esta sub tarefa?", "Sim", "Não");
            if (!result) return;

            var updateResponse = await _subTaskService.ReactivateSubTask(_taskId);

            if (updateResponse.Sucess)
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Sub tarefa reativada com sucesso!", 3000));
            else
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", updateResponse.Message, 5000));

            WeakReferenceMessenger.Default.Send(new SubTaskBottomSheetClosedMessage("Close BottomSheet"));
        }
    }
}

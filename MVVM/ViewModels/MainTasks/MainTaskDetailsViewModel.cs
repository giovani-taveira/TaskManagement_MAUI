using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagement.Helpers.Enums;
using TaskManagement.MVVM.Views._Components;
using TaskManagement.Services.Interfaces;
using static TaskManagement.Helpers.Messages.MainTaskMessages;

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
        public async Task GetMainTaskById()
        {
            var task = await _mainTaskService.GetMainTasksById(_taskId);

            if (task != null)
            {
                Title = task.Title;
                Description = task.Description;
                CreatedAt = task.CreatedAt?.ToString("dd/MM/yy");
                ConcludedAt = task.ConcludedAt?.ToString("dd/MM/yy");

                if(task.Status == StatusEnum.Concluido.ToString())
                {
                    IsCompleteButtonEnabled = false;
                    IsReactivateButtonEnabled = true;
                }             
            }
        }

        [RelayCommand]
        public async Task DeleteMainTask()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Atenção", "Tem certeza que deseja deletar a tarefa? Essa ação não poderá ser desfeita!", "Sim", "Não");
            if (!result) return;

            var deleteResponse = await _mainTaskService.DeleteMainTask(_taskId);

            if (deleteResponse.Sucess)
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Tarefa deletada com sucesso!", 3000));
            else
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", deleteResponse.Message, 5000));

            WeakReferenceMessenger.Default.Send(new BottomSheetClosedMessage("Close BottomSheet"));
        }

        [RelayCommand]
        public async Task CompleteMainTask()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Atenção", "Esta ação irá concluir automaticamente todas as subtarefas ativas! deseja prosseguir mesmo assim?", "Sim", "Não");
            if (!result) return;

            var updateResponse = await _mainTaskService.CompleteMainTask(_taskId);

            if (updateResponse.Sucess)
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Tarefa concluída com sucesso!", 3000));
            else
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", updateResponse.Message, 5000));

            WeakReferenceMessenger.Default.Send(new BottomSheetClosedMessage("Close BottomSheet"));
        }

        [RelayCommand]
        public async Task ReactivateMainTask()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Atenção", "Deseja reativar esta tarefa?", "Sim", "Não");
            if (!result) return;

            var updateResponse = await _mainTaskService.ReactivateMainTask(_taskId);

            if (updateResponse.Sucess)
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("sucess.gif", "Tarefa reativada com sucesso!", 3000));
            else
                await Application.Current.MainPage.ShowPopupAsync(new CustomPopup("error.gif", updateResponse.Message, 5000));

            WeakReferenceMessenger.Default.Send(new BottomSheetClosedMessage("Close BottomSheet"));
        }
    }
}

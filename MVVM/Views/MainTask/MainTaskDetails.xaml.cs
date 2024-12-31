using CommunityToolkit.Mvvm.Messaging;
using TaskManagement.MVVM.ViewModels.MainTasks;
using TaskManagement.Services.Interfaces;
using The49.Maui.BottomSheet;
using static TaskManagement.Helpers.Messages.MainTaskMessages;

namespace TaskManagement.MVVM.Views.MainTask;

public partial class MainTaskDetails : BottomSheet
{
	private readonly Guid _mainTaskId;
    private readonly IMainTaskService _mainTaskService;

    public MainTaskDetails(Guid mainTaskId, 
        IMainTaskService mainTaskService)
    {
        _mainTaskId = mainTaskId;
        _mainTaskService = mainTaskService;

        BindingContext = new MainTaskDetailsViewModel(mainTaskService, mainTaskId);

        InitializeComponent();

        WeakReferenceMessenger.Default.Register<BottomSheetClosedMessage>(this, (r, message) =>
        {
            OnCloseBottomSheetRequested();
        });
    }

    private async void btnEdit_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new OpenEditFormMessage(_mainTaskId));
        await this.DismissAsync();
    }

    private async void OnCloseBottomSheetRequested()
    {
        WeakReferenceMessenger.Default.Send(new GetAllMainTasksMessage("Get all main tasks"));
        await this.DismissAsync();
    }

    private async void btnSubtasks_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new OpenSubtasksPageMessage(_mainTaskId));
        await this.DismissAsync();
    }
}
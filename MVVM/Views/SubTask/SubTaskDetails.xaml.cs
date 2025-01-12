using CommunityToolkit.Mvvm.Messaging;
using TaskManagement.MVVM.ViewModels.SubTasks;
using TaskManagement.Services.Interfaces;
using The49.Maui.BottomSheet;
using static TaskManagement.Helpers.Messages.SubTaskMessages;

namespace TaskManagement.MVVM.Views.SubTask;

public partial class SubTaskDetails : BottomSheet
{
    private readonly Guid _subTaskId;
    public SubTaskDetails(Guid subTaskId, ISubTaskService subTaskService)
    {
        _subTaskId = subTaskId;

        InitializeComponent();

        BindingContext = new SubTaskDetailsViewModel(subTaskService, subTaskId);

        WeakReferenceMessenger.Default.Register<SubTaskBottomSheetClosedMessage>(this, (r, message) =>
        {
            OnCloseBottomSheetRequested();
        });
    }

    private async void btnEdit_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new OpenEditSubTaskFormMessage(_subTaskId));
        await this.DismissAsync();
    }

    private async void OnCloseBottomSheetRequested()
    {
        WeakReferenceMessenger.Default.Send(new GetAllSubTasksMessage("Get all sub tasks"));
        await this.DismissAsync();
    }
}
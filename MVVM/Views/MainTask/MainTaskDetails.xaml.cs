using TaskManagement.MVVM.ViewModels.MainTasks;
using TaskManagement.Services.Interfaces;
using The49.Maui.BottomSheet;

namespace TaskManagement.MVVM.Views.MainTask;

public partial class MainTaskDetails : BottomSheet
{
	private readonly Guid _mainTaskId;

    public MainTaskDetails(Guid mainTaskId, 
        IMainTaskService mainTaskService)
    {
        _mainTaskId = mainTaskId;

        BindingContext = new MainTaskDetailsViewModel(mainTaskService, mainTaskId);

        InitializeComponent();
    }
}
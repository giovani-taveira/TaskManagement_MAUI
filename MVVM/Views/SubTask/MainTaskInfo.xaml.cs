using TaskManagement.MVVM.ViewModels.MainTasks;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.Views.SubTask;

public partial class MainTaskInfo : ContentView
{
    private readonly Guid _mainTaskId;
    private readonly IMainTaskService _mainTaskService;
    public MainTaskInfo(Guid mainTaskId,
        IMainTaskService mainTaskService)
	{
        _mainTaskId = mainTaskId;
        _mainTaskService = mainTaskService;
        InitializeComponent();
		BindingContext = new MainTaskDetailsViewModel(mainTaskService, mainTaskId);
    }

    private void CloseModalButton_Clicked(object sender, EventArgs e)
    {

    }
}
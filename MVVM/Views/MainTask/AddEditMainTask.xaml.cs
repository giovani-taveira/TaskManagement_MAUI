using CommunityToolkit.Mvvm.ComponentModel;
using TaskManagement.MVVM.ViewModels.MainTasks;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.Views.MainTask;


public partial class AddEditMainTask : ContentPage
{
	public readonly IMainTaskService _mainTaskService;
    
    public AddEditMainTask(IMainTaskService mainTaskService)
	{
        InitializeComponent();
        _mainTaskService = mainTaskService;

        BindingContext = new AddEditTaskViewModel(mainTaskService, this.Navigation);
    }
}
using TaskManagement.MVVM.ViewModels.SubTasks;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.Views.SubTask;

public partial class AddEditSubTask : ContentPage
{
	public AddEditSubTask(ISubTaskService subTaskService, Guid? subTaskId, Guid mainTaskId)
	{
		InitializeComponent();

		BindingContext = new AddEditSubTaskViewModel(subTaskService, this.Navigation, subTaskId, mainTaskId);
	}
}
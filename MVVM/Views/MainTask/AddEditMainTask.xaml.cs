using TaskManagement.MVVM.ViewModels.MainTasks;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.Views.MainTask;

public partial class AddEditMainTask : ContentPage
{
	public AddEditMainTask(IMainTaskService mainTaskService)
	{
		InitializeComponent();

		BindingContext = new AddEditTaskViewModel(mainTaskService, this.Navigation);

        datepicker_deadline.MinimumDate = DateTime.Now;
    }
}
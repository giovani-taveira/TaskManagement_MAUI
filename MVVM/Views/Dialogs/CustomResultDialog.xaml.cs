using TaskManagement.MVVM.ViewModels.MainTasks;
using TaskManagement.MVVM.Views._Components;

namespace TaskManagement.MVVM.Views.Dialogs;

public partial class CustomResultDialog : ContentView
{
    CustomPopup _mypopup;
	public CustomResultDialog(CustomPopup popUp, string imagePath, string message, int closeAfter)
	{
		InitializeComponent();
        _mypopup = popUp;

        dialog_image.Source = imagePath;
        dialog_label.Text = message;

        CloseAfterDelayAsync(closeAfter);
    }

    private async void CloseAfterDelayAsync(int miliseconds)
    {
        await Task.Delay(miliseconds);
        _mypopup.Close(); 
    }
}
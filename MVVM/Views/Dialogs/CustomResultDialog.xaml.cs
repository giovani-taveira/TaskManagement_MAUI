using CommunityToolkit.Mvvm.Messaging;
using TaskManagement.MVVM.Views._Components;
using static TaskManagement.Helpers.Messages.AppSharedMessages;

namespace TaskManagement.MVVM.Views.Dialogs;

public partial class CustomResultDialog : ContentView
{
    CustomPopup _mypopup;
    private bool isDismissed = false;

	public CustomResultDialog(CustomPopup popUp, string imagePath, string message, int closeAfter)
	{
		InitializeComponent();
        _mypopup = popUp;

        dialog_image.Source = imagePath;
        dialog_label.Text = message;

        WeakReferenceMessenger.Default.Register<DismissedCustomPopupMessage>(this, (r, message) =>
        {
            isDismissed = true;
        });

        CloseAfterDelayAsync(closeAfter);
    }

    private async void CloseAfterDelayAsync(int miliseconds)
    {
        await Task.Delay(miliseconds);

        if (!isDismissed)
        {
            await _mypopup.CloseAsync();
        }      
    }
}
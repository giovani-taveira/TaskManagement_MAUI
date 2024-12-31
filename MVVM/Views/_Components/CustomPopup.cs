using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagement.MVVM.Views.Dialogs;
using static TaskManagement.Helpers.Messages.AppSharedMessages;

namespace TaskManagement.MVVM.Views._Components
{
    public class CustomPopup : Popup
    {
        public CustomPopup(string imagePath, string message, int closeAfter)
        {
            Content = new CustomResultDialog(this, imagePath, message, closeAfter);
            CanBeDismissedByTappingOutsideOfPopup = true;
        }

        protected override Task OnClosed(object result, bool wasDismissedByTappingOutsideOfPopup, CancellationToken token = default)
        {
            WeakReferenceMessenger.Default.Send(new DismissedCustomPopupMessage("Dismissed Custom Popup"));
            return base.OnClosed(result, wasDismissedByTappingOutsideOfPopup, token);
        }
    }
}

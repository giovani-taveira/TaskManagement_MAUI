using CommunityToolkit.Maui.Views;
using TaskManagement.MVVM.Views.Dialogs;

namespace TaskManagement.MVVM.Views._Components
{
    public class CustomPopup : Popup
    {
        public CustomPopup(string imagePath, string message, int closeAfter)
        {
            Content = new CustomResultDialog(this, imagePath, message, closeAfter);
        }
    }
}

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TaskManagement.Helpers.Messages
{
    public class AppSharedMessages
    {
        public class DismissedCustomPopupMessage : ValueChangedMessage<string>
        {
            public DismissedCustomPopupMessage(string message) : base(message) { }
        }
    }
}

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TaskManagement.Helpers.Messages
{
    public static class MainTaskMessages
    {
        public class BottomSheetClosedMessage : ValueChangedMessage<string>
        {
            public BottomSheetClosedMessage(string message) : base(message) { }
        }

        public class GetAllMainTasksMessage : ValueChangedMessage<string>
        {
            public GetAllMainTasksMessage(string message) : base(message) { }
        }

        public class OpenEditFormMessage : ValueChangedMessage<Guid>
        {
            public OpenEditFormMessage(Guid message) : base(message) { }
        }

        public class OpenSubtasksPageMessage : ValueChangedMessage<Guid>
        {
            public OpenSubtasksPageMessage(Guid message) : base(message) { }
        }
    }
}

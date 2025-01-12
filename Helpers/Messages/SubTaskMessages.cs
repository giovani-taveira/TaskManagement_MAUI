using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Helpers.Messages
{
    public class SubTaskMessages
    {
        public class SubTaskBottomSheetClosedMessage : ValueChangedMessage<string>
        {
            public SubTaskBottomSheetClosedMessage(string message) : base(message) { }
        }

        public class GetAllSubTasksMessage : ValueChangedMessage<string>
        {
            public GetAllSubTasksMessage(string message) : base(message) { }
        }

        public class OpenEditSubTaskFormMessage : ValueChangedMessage<Guid>
        {
            public OpenEditSubTaskFormMessage(Guid message) : base(message) { }
        }

        public class OpenSubtasksPageMessage : ValueChangedMessage<Guid>
        {
            public OpenSubtasksPageMessage(Guid message) : base(message) { }
        }
    }
}

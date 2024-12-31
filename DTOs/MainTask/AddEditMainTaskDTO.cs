using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DTOs.MainTask
{
    public record AddEditMainTaskDTO(
        Guid? Id,
        string Title,
        string Description,
        DateTime? DeadlineDate,
        string Status,
        bool IsNotifiable
        )
    {
    }
}

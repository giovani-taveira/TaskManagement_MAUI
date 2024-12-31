using TaskManagement.MVVM.Views._Components;

namespace TaskManagement.DTOs.MainTask
{
    public record MainTaskDTO(
        Guid Id,
        string Title,
        string Description,
        DateTime? DeadlineDate,
        string Status,
        string QtdSubTasks,
        bool IsNotifiable,
        double ProgressDrawable,
        CircularProgressDrawable CircularProgressDrawableInstance
        );
}

using TaskManagement.MVVM.Views._Components;

namespace TaskManagement.DTOs.MainTask
{
    public record MainTaskDTO(
        Guid Id,
        string Title,
        string Description,
        string DeadlineDate,
        string Status,
        string QtdSubTasks,
        double ProgressDrawable,
        CircularProgressDrawable CircularProgressDrawableInstance
        );
}

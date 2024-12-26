using TaskManagement.MVVM.Views._Components;

namespace TaskManagement.DTOs.MainTask
{
    public record MainTaskDTO(string Title,
        string DeadlineDate,
        string Status,
        string QtdSubTasks,
        double ProgressDrawable,
        CircularProgressDrawable CircularProgressDrawableInstance);
}

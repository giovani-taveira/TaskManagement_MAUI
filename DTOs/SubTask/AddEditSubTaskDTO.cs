namespace TaskManagement.DTOs.SubTask
{
    public record class AddEditSubTaskDTO(
        Guid? Id,
        Guid MainTaskId,
        string Title,
        string Description,
        string Status,
        DateTime? DeadlineDate
        )
    { }
}

namespace TaskManagement.DTOs.SubTask
{
    public record class SubTaskDTO(
        Guid Id,
        Guid MainTaskId,
        DateTime? DeadlineDate,
        DateTime? CreatedAt,
        DateTime? ConcludedAt,
        string Title,
        string Description,
        string Status)
    { }
}

using TaskManagement.DTOs;
using TaskManagement.DTOs.SubTask;

namespace TaskManagement.Services.Interfaces
{
    public interface ISubTaskService
    {
        Task<List<SubTaskDTO>> GetAllSubTasks(Guid mainTaskId);
        Task<SubTaskDTO> GetSubTaskById(Guid subTaskId);
        Task<BaseResponseDTO> CreateSubTask(AddEditSubTaskDTO model);
        Task<BaseResponseDTO> UpdateSubTask(AddEditSubTaskDTO model);
        Task<BaseResponseDTO> DeleteSubTask(Guid id);
        Task<BaseResponseDTO> CompleteSubTask(Guid id);
        Task<BaseResponseDTO> ReactivateSubTask(Guid id);
    }
}

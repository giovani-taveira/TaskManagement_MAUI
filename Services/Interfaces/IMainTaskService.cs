using TaskManagement.DTOs;
using TaskManagement.DTOs.MainTask;
using TaskManagement.MVVM.Models;

namespace TaskManagement.Services.Interfaces
{
    public interface IMainTaskService
    {
        Task<List<MainTaskDTO>> GetAllMainTasks();
        Task<MainTaskDTO> GetMainTasksById(Guid id);
        Task<BaseResponseDTO> CreateMainTask(AddEditMainTaskDTO model);
        Task<BaseResponseDTO> UpdateMainTask(AddEditMainTaskDTO model);
        Task<BaseResponseDTO> DeleteMainTask(Guid id);
        Task<BaseResponseDTO> CompleteMainTask(Guid id);
        Task<BaseResponseDTO> ReactivateMainTask(Guid id);
    }
}

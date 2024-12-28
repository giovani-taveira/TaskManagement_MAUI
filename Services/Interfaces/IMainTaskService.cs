using TaskManagement.DTOs;
using TaskManagement.DTOs.MainTask;
using TaskManagement.MVVM.Models;

namespace TaskManagement.Services.Interfaces
{
    public interface IMainTaskService
    {
        Task<BaseResponseDTO> CreateMainTask(MainTask task);
        Task<List<MainTaskDTO>> GetAllMainTasks();
        Task<MainTaskDTO> GetMainTasksById(Guid id);
    }
}

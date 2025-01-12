using System.Threading.Tasks;
using TaskManagement.DTOs;
using TaskManagement.DTOs.SubTask;
using TaskManagement.Helpers.Enums;
using TaskManagement.MVVM.Models;
using TaskManagement.MVVM.Models.DomainObjects;
using TaskManagement.Persistence.Respositories;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class SubTaskService : ISubTaskService
    {
        private readonly IRepository<SubTask> _repository;
        private readonly IRepository<MainTask> _mainTaskRepository;

        public SubTaskService(IRepository<SubTask> repository, 
            IRepository<MainTask> mainTaskRepository)
        {
            _repository = repository;
            _mainTaskRepository = mainTaskRepository;
        }

        public async Task<SubTaskDTO> GetSubTaskById(Guid subTaskId)
        {
            var subTask = await _repository.GetByIdAsync(subTaskId);
            var subTaskStatus = subTask.Status == StatusEnum.Ativo.ToString() && subTask.DeadlineDate.HasValue && DateTime.Today > subTask.DeadlineDate.Value
                            ? StatusEnum.Em_Atraso.ToString().Replace("_", " ") : subTask.Status;

            return new SubTaskDTO
            (
                Id: subTask.Id,
                MainTaskId: subTask.MainTaskId,
                DeadlineDate: subTask.DeadlineDate,
                CreatedAt: subTask.CreatedAt,
                ConcludedAt: subTask.ConcludedAt,
                Title: subTask.Title,
                Description: subTask.Description,
                Status: subTaskStatus
            );
        }

        public async Task<List<SubTaskDTO>> GetAllSubTasks(Guid mainTaskId)
        {
            var subTasks = await _repository.SearchAsync(x => x.MainTaskId.Equals(mainTaskId));

            return subTasks.Select(x => new SubTaskDTO
            (
                Id: x.Id,
                MainTaskId: x.MainTaskId,
                DeadlineDate: x.DeadlineDate,
                CreatedAt: x.CreatedAt,
                ConcludedAt: x.ConcludedAt,
                Title: x.Title,
                Description: x.Description,
                Status: x.Status
            )).ToList();
        }

        public async Task<BaseResponseDTO> CreateSubTask(AddEditSubTaskDTO model)
        {
            try
            {
                var task = new SubTask
                {
                    MainTaskId = model.MainTaskId,
                    Title = model.Title,
                    Description = model.Description,
                    Status = model.Status,
                    DeadlineDate = model.DeadlineDate
                };

                var response = await _repository.CreateAsync(task);

                if (response == 1)
                    return new BaseResponseDTO { Sucess = true };
                else
                    return new BaseResponseDTO { Sucess = false, Message = "Erro ao salvar a sub tarefa" };
            }
            catch (DomainException ex)
            {
                return new BaseResponseDTO { Sucess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponseDTO> UpdateSubTask(AddEditSubTaskDTO model)
        {
            try
            {
                var task = await _repository.GetByIdAsync(model.Id.Value);
                if (task == null) new BaseResponseDTO { Sucess = false, Message = "Sub tarefa não encontrada" };

                task.Title = model.Title;
                task.Description = model.Description;
                task.DeadlineDate = model.DeadlineDate;

                var response = await _repository.UpdateAsync(task);

                if (response == 1)
                    return new BaseResponseDTO { Sucess = true };
                else
                    return new BaseResponseDTO { Sucess = false, Message = "Erro ao atualizar a sub tarefa" };
            }
            catch (DomainException ex)
            {
                return new BaseResponseDTO { Sucess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponseDTO> DeleteSubTask(Guid id)
        {
            var subTask = await _repository.GetByIdAsync(id);
            var response = await _repository.DeleteAsync(subTask);

            if (response == 1)
                return new BaseResponseDTO { Sucess = true };
            else
                return new BaseResponseDTO { Sucess = false, Message = "Erro ao deletar a sub tarefa" };
        }

        public async Task<BaseResponseDTO> CompleteSubTask(Guid id)
        {
            try
            {
                var subTask = await _repository.GetByIdAsync(id);               

                subTask.Status = StatusEnum.Concluido.ToString();
                subTask.ConcludedAt = DateTime.Now;
                var responseMainUpdate = await _repository.UpdateAsync(subTask);

                var allSubTasks = await _repository.SearchAsync(x => x.MainTaskId.Equals(subTask.MainTaskId));

                if (allSubTasks.All(x => x.Status.Equals(StatusEnum.Concluido.ToString())))
                {
                    var mainTask = await _mainTaskRepository.GetByIdAsync(subTask.MainTaskId);
                    mainTask.Status = StatusEnum.Concluido.ToString();
                    mainTask.ConcludedAt = DateTime.Now;
                    await _mainTaskRepository.UpdateAsync(mainTask);
                }                  

                if (responseMainUpdate == 1)
                    return new BaseResponseDTO { Sucess = true };
                else
                    return new BaseResponseDTO { Sucess = false, Message = "Erro ao atualizar a sub tarefa" };
            }
            catch (DomainException ex)
            {
                return new BaseResponseDTO { Sucess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponseDTO> ReactivateSubTask(Guid id)
        {
            var subTask = await _repository.GetByIdAsync(id);
            subTask.Status = StatusEnum.Ativo.ToString();
            subTask.ConcludedAt = null;
            var responseUpdate = await _repository.UpdateAsync(subTask);

            var mainTask = await _mainTaskRepository.GetByIdAsync(subTask.MainTaskId);

            if (mainTask.Status.Equals(StatusEnum.Concluido.ToString()))
            {
                mainTask.Status = StatusEnum.Ativo.ToString();
                mainTask.ConcludedAt = null;
                await _mainTaskRepository.UpdateAsync(mainTask);
            }

            if (responseUpdate == 1)
                return new BaseResponseDTO { Sucess = true };
            else
                return new BaseResponseDTO { Sucess = false, Message = "Erro ao atualizar a sub tarefa" };
        }
    }
}

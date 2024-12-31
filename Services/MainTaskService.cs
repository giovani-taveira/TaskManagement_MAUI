using TaskManagement.DTOs;
using TaskManagement.DTOs.MainTask;
using TaskManagement.Helpers.Enums;
using TaskManagement.MVVM.Models;
using TaskManagement.MVVM.Models.DomainObjects;
using TaskManagement.MVVM.Views._Components;
using TaskManagement.Persistence.Respositories;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class MainTaskService : IMainTaskService
    {
        private readonly IRepository<MainTask> _repository;
        private readonly IRepository<SubTask> _subTaskRepository;
        public MainTaskService(IRepository<MainTask> repository, IRepository<SubTask> subTaskRepository)
        {
            _repository = repository;
            _subTaskRepository = subTaskRepository;
        }

        public async Task<MainTaskDTO> GetMainTasksById(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);

            var subTasks = await _subTaskRepository.SearchAsync(x => x.TaskId.Equals(task.Id));
            var taskStatus = task.Status == StatusEnum.Ativo.ToString() && task.DeadlineDate.HasValue && DateTime.Today > task.DeadlineDate.Value
                                            ? StatusEnum.Em_Atraso.ToString().Replace("_", " ") : task.Status;

            var concludedTask = subTasks.Where(x => x.Status.Equals(StatusEnum.Concluido.ToString()));

            return new MainTaskDTO
            (
                Id: task.Id,
                Title: task.Title,
                Description: task.Description,
                DeadlineDate: task.DeadlineDate,
                Status: taskStatus,
                IsNotifiable: task.IsNotifiable,
                QtdSubTasks: subTasks.Any() ? $"{subTasks.Where(x => x.Status.Equals(StatusEnum.Concluido.ToString())).Count()}/{subTasks.Count()}" : "0/0",
                ProgressDrawable: 0,
                CircularProgressDrawableInstance: null
            );
        }

        public async Task<List<MainTaskDTO>> GetAllMainTasks()
        {
            var tasks = await _repository.GetAll();
            var tasksList = new List<MainTaskDTO>();

            foreach (var task in tasks)
            {
                var subTasks = await _subTaskRepository.SearchAsync(x => x.TaskId.Equals(task.Id));
                var taskStatus = task.Status == StatusEnum.Ativo.ToString() && task.DeadlineDate.HasValue && DateTime.Today > task.DeadlineDate.Value
                                                ? StatusEnum.Em_Atraso.ToString().Replace("_", " ") : task.Status;

                var concludedTask = subTasks.Where(x => x.Status.Equals(StatusEnum.Concluido.ToString()));

                tasksList.Add(new MainTaskDTO
                (
                    Id: task.Id,
                    Title: task.Title,
                    Description: task.Description,
                    DeadlineDate: task.DeadlineDate,
                    Status: taskStatus,
                    IsNotifiable: task.IsNotifiable,
                    QtdSubTasks: subTasks.Any() ? $"{subTasks.Where(x => x.Status.Equals(StatusEnum.Concluido.ToString())).Count()}/{subTasks.Count()}" : "0/0",
                    ProgressDrawable: concludedTask.Count() > 0 && subTasks.Count() > 0 ? subTasks.Where(x => x.Status.Equals(StatusEnum.Concluido.ToString())).Count() / subTasks.Count() : 0,
                    CircularProgressDrawableInstance: new CircularProgressDrawable()
                ));
            }

            return tasksList;   
        }

        public async Task<BaseResponseDTO> CreateMainTask(AddEditMainTaskDTO model)
        {
            try
            {
                var task = new MainTask
                {
                    Title = model.Title,
                    Description = model.Description,
                    DeadlineDate = model.DeadlineDate,
                    Status = model.Status,
                    IsNotifiable = model.IsNotifiable,
                };

                var response = await _repository.CreateAsync(task);

                if (response == 1)
                    return new BaseResponseDTO { Sucess = true };
                else
                    return new BaseResponseDTO { Sucess = false, Message = "Erro ao salvar a tarefa" };
            }
            catch(DomainException ex) 
            {
                return new BaseResponseDTO { Sucess = false, Message = ex.Message };
            }         
        }

        public async Task<BaseResponseDTO> UpdateMainTask(AddEditMainTaskDTO model)
        {
            try
            {
                var task = await _repository.GetByIdAsync(model.Id.Value);
                if (task == null) new BaseResponseDTO { Sucess = false, Message = "Tarefa não encontrada" };

                task.Title = model.Title;
                task.Description = model.Description;
                task.DeadlineDate = model.DeadlineDate;
                task.IsNotifiable = model.IsNotifiable;

                var response = await _repository.UpdateAsync(task);

                if (response == 1)
                    return new BaseResponseDTO { Sucess = true };
                else
                    return new BaseResponseDTO { Sucess = false, Message = "Erro ao salvar a tarefa" };
            }
            catch (DomainException ex)
            {
                return new BaseResponseDTO { Sucess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponseDTO> DeleteMainTask(Guid id)
        {
            var mainTask = await _repository.GetByIdAsync(id);
            var response = await _repository.DeleteAsync(mainTask);

            if (response == 1)
                return new BaseResponseDTO { Sucess = true };
            else
                return new BaseResponseDTO { Sucess = false, Message = "Erro ao deletar a tarefa" };
        }

        public async Task<BaseResponseDTO> CompleteMainTask(Guid id)
        {
            try
            {
                var mainTask = await _repository.GetByIdAsync(id);
                var subtasks = await _subTaskRepository.SearchAsync(x => x.TaskId.Equals(id));

                var subtasksToUpdate = new List<SubTask>();

                if (subtasks.Any())
                {
                    foreach (var subtask in subtasks)
                    {
                        if (subtask.Status.Equals(StatusEnum.Cancelado.ToString())) continue;

                        subtask.Status = StatusEnum.Concluido.ToString();
                        subtasksToUpdate.Add(subtask);
                    }

                    var response = await _subTaskRepository.UpdateRangeAsync(subtasksToUpdate);

                    if (response == 0) return new BaseResponseDTO { Sucess = false, Message = "Erro ao atualizar a tarefa" };
                }
                
                mainTask.Status = StatusEnum.Concluido.ToString();
                var resposnseMainUpdate = await _repository.UpdateAsync(mainTask);

                if (resposnseMainUpdate == 1)
                    return new BaseResponseDTO { Sucess = true };
                else
                    return new BaseResponseDTO { Sucess = false, Message = "Erro ao atualizar a tarefa" };
                
            }
            catch (DomainException ex)
            {
                return new BaseResponseDTO { Sucess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponseDTO> ReactivateMainTask(Guid id)
        {
            try
            {
                var mainTask = await _repository.GetByIdAsync(id);
                mainTask.Status = StatusEnum.Ativo.ToString();
                var resposnseMainUpdate = await _repository.UpdateAsync(mainTask);

                if (resposnseMainUpdate == 1)
                    return new BaseResponseDTO { Sucess = true };
                else
                    return new BaseResponseDTO { Sucess = false, Message = "Erro ao atualizar a tarefa" };

            }
            catch (DomainException ex)
            {
                return new BaseResponseDTO { Sucess = false, Message = ex.Message };
            }
        }
    }
}

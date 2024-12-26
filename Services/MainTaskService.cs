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
                    Title: task.Title,
                    DeadlineDate: task.DeadlineDate.Value.ToString("dd/MM/yyyy") ?? "Sem Prazo",
                    Status: taskStatus,
                    QtdSubTasks: subTasks.Any() ? $"{subTasks.Where(x => x.Status.Equals(StatusEnum.Concluido.ToString())).Count()}/{subTasks.Count()}" : "0/0",
                    ProgressDrawable: concludedTask.Count() > 0 && subTasks.Count() > 0 ? subTasks.Where(x => x.Status.Equals(StatusEnum.Concluido.ToString())).Count() / subTasks.Count() : 0,
                    CircularProgressDrawableInstance: new CircularProgressDrawable()
                ));
            }

            return tasksList;   
        }

        public async Task<BaseResponseDTO> CreateMainTask(MainTask task)
        {
            try
            {
                if (task == null) new BaseResponseDTO { Sucess = false, Message = "Dados não encontrados" };

                var response = await _repository.CreateAsync(task);

                if (response == 1)
                    return new BaseResponseDTO { Sucess = true };
                else
                    return new BaseResponseDTO { Sucess = false, Message = "Erro ao salvar os dados" };
            }
            catch(DomainException ex) 
            {
                return new BaseResponseDTO { Sucess = false, Message = ex.Message };
            }         
        }
    }
}

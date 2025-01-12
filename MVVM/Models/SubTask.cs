using SQLite;
using TaskManagement.MVVM.Models.DomainObjects;

namespace TaskManagement.MVVM.Models
{
    [Table("SubTasks")]
    public class SubTask : Entity
    {
        [NotNull]
        public Guid MainTaskId { get; set; }

        [NotNull, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [NotNull, MaxLength(50)]
        public string Status { get; set; }

        [NotNull]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? DeadlineDate { get; set; }

        public DateTime? ConcludedAt { get; set; }

        public override void Validate()
        {
            Validations.ValidateGuidIsNotNull(MainTaskId, "Não foi encontrada a tarefa principal");
            Validations.ValidateLength(Title, 100, "O título da sub tarefa não pode ser maior que 100 caracteres");
            Validations.ValidateLength(Description, 500, "A descrição da sub tarefa não pode ser maior que 500 caracteres");
            Validations.ValidateLength(Status, 50, "O status da sub tarefa não pode ser maior que 50 caracteres");
        }
    }
}

using SQLite;
using TaskManagement.MVVM.Models.DomainObjects;

namespace TaskManagement.MVVM.Models
{
    [Table("MainTasks")]
    public class MainTask : Entity
    {
        [NotNull, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime? DeadlineDate { get; set; }

        [NotNull, MaxLength(50)]
        public string Status { get; set; }

        [NotNull]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        public DateTime? ConcludedAt { get; set; }

        [NotNull]
        public bool IsNotifiable { get; set; }


        public override void Validate()
        {
            Validations.ValidateLength(Title, 100, "O título da tarefa não pode ser maior que 100 caracteres!");
            Validations.ValidateLength(Description, 500, "A descrição da tarefa não pode ser maior que 500 caracteres!");
            Validations.ValidateLength(Status, 500, "O Status da tarefa não pode ser maior que 50 caracteres!");
            Validations.ValidateDateTimeIsNotMinOrMaxValue(DeadlineDate, "Valor do prazo de finalização inválido!");
        }
    }
}

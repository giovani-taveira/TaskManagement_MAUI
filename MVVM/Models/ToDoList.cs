using SQLite;

namespace TaskManagement.MVVM.Models
{
    [Table("ToDoListItems")]
    public class ToDoList : Entity
    {
        [NotNull, MaxLength(100)]
        public string Title { get; set; }

        [NotNull, MaxLength(50)]
        public string Status { get; set; }

        public bool IsNotifiable { get; set; }

        public DateTime DeadlineDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}

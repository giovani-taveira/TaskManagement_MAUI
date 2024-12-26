using SQLite;

namespace TaskManagement.MVVM.Models
{
    [Table("ToDoListItems")]
    public class ToDoListItem : Entity
    {
        [NotNull, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [NotNull]
        public Guid ToDoListId { get; set; }

        [NotNull]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}

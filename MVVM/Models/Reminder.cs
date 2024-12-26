using SQLite;

namespace TaskManagement.MVVM.Models
{
    [Table("Reminders")]
    public class Reminder : Entity
    {
        [NotNull, MaxLength(100)]
        public string Title { get; set; }

        [NotNull, MaxLength(500)]
        public string Description { get; set; }

        [NotNull, MaxLength(50)]
        public string Status { get; set; }

        [NotNull]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}

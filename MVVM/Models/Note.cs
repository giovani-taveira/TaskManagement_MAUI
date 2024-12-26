using SQLite;

namespace TaskManagement.MVVM.Models
{
    [Table("Notes")]
    public class Note : Entity
    {
        public Guid? TaskId { get; set; }

        [NotNull]
        public string Content { get; set; }

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

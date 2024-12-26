using SQLite;

namespace TaskManagement.MVVM.Models
{
    public abstract class Entity
    {
        [PrimaryKey]
        public Guid Id { get; init; } = Guid.NewGuid();


        public abstract void Validate();
    }
}

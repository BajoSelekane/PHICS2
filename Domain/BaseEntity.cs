using Domain.Enums;


namespace Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public  string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public EntityStatus Status { get; set; }

    }
}

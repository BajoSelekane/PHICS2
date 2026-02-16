using Domain.Enums;
using MediatR;


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

        private readonly List<INotification> _domainEvents = new();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        // Add a domain event
        public void AddDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);

        // Clear events after publishing
        public void ClearDomainEvents() => _domainEvents.Clear();

    }


}

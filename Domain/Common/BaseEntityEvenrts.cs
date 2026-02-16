using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
  
    public abstract class BaseEntityEvenrts
    {
        private readonly List<DomainEvent> _domainEvents = new();

        public IReadOnlyCollection<DomainEvent> DomainEvents =>
            _domainEvents.AsReadOnly();

        public void AddDomainEvent(DomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents.Clear();
    }
}

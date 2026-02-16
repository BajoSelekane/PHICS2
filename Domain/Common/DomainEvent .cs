using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Domain.Common
{




    public abstract class DomainEvent : INotification
    {
        public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
    }

}

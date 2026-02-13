using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Events
{


    public sealed class AppointmentBookedEvent : DomainEvent
    {
        public Appointment Appointment { get; }

        public AppointmentBookedEvent(Appointment appointment)
        {
            Appointment = appointment;
        }
    }

}

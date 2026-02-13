using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Appointments.Commands
{
    public record BookAppointmentCommand : IRequest<BookAppointmentResult>
    {
        public Guid DoctorId { get; init; }
        public Guid TimeSlotId { get; init; }
        public DateTime AppointmentDate { get; init; }
        public DateTime DOB {  get; init; } 
        public string Appointment_Description { get; init; } = string.Empty;
        public string PatientName { get; init; } = string.Empty;
        public string PatientEmail { get; init; } = string.Empty;
        public string PatientPhone { get; init; } = string.Empty;
        public string PatientIDNumber { get; init; } = string.Empty;
    }

    public record BookAppointmentResult
    {
        public Guid AppointmentId { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}

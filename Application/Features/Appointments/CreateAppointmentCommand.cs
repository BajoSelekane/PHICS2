using Application.Abstractions.Messaging;


namespace Application.Features.Appointments
{
    public sealed record CreateAppointmentCommand(
     Guid PatientId,
     Guid TimeSlotId,
     Guid? DoctorId,
     string PhoneNumber,
     DateTime AppointmentDate,
     TimeSpan EndTime,
     string? Description
 ) : ICommand<Guid>;

}

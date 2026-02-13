
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Shared;


namespace Application.Features.Appointments
{
    public sealed class CreateAppointmentHandler
      : ICommandHandler<CreateAppointmentCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateAppointmentHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            CreateAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var timeSlot = await _context.TimeSlots
                .FirstOrDefaultAsync(t => t.Id == request.TimeSlotId, cancellationToken);

            if (timeSlot is null || !timeSlot.IsAvailable)
                throw new Exception("TimeSlot is not available.");

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = request.PatientId,
                TimeSlotId = request.TimeSlotId,
                DoctorId = request.DoctorId,
                PhoneNumber = request.PhoneNumber,
                AppointmentDate = request.AppointmentDate,
                EndTime = request.EndTime,
                Appointment_Description = request.Description
            };

            timeSlot.IsAvailable = false;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync(cancellationToken);

            return appointment.Id;
        }

        Task<Result<Guid>> IRequestHandler<CreateAppointmentCommand, Result<Guid>>.Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}

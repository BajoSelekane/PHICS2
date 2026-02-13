using Application.Abstractions.Data;
using Application.Interfaces;
using Domain;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Domain.Common.Events;

namespace Application.Features.Appointments.Commands
{
    public class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, BookAppointmentResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITwilioService _twilioService;
        private readonly ILogger<BookAppointmentCommandHandler> _logger;

        public BookAppointmentCommandHandler(
            IApplicationDbContext context,
            ITwilioService twilioService,
            ILogger<BookAppointmentCommandHandler> logger)
        {
            _context = context;
            _twilioService = twilioService;
            _logger = logger;
        }

        public async Task<BookAppointmentResult> Handle(
     BookAppointmentCommand request,
     CancellationToken cancellationToken)
        {
            await using var transaction =
                await _context.BeginTransactionAsync(cancellationToken);

            try
            {
                // 1️⃣ Validate TimeSlot
                var timeSlot = await _context.TimeSlots
                    .Include(ts => ts.Doctor)
                    .FirstOrDefaultAsync(ts =>
                        ts.Id == request.TimeSlotId &&
                        ts.IsAvailable,
                        cancellationToken);

                if (timeSlot is null)
                    throw new InvalidOperationException("This time slot is no longer available.");

                // 2️⃣ Create or Retrieve Patient
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p =>
                        p.IDNumber == request.PatientIDNumber,
                        cancellationToken);

                if (patient is null)
                {
                    patient = new Patient
                    {
                        Id = Guid.NewGuid(),
                        IDNumber = request.PatientIDNumber,
                        PatientName = request.PatientName,
                        Email = request.PatientEmail,
                        PhoneNumber = request.PatientPhone,
                        DateOfBirth = request.DOB,
                        Appointment_Description = request.Appointment_Description
                    };

                    _context.Patients.Add(patient);
                }

                // 3️⃣ Create Appointment
                var appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    PatientId = patient.Id,
                    TimeSlotId = timeSlot.Id,
                    AppointmentDate = request.AppointmentDate,
                    EndTime = timeSlot.EndTime,
                    PhoneNumber = request.PatientPhone,
                    Appointment_Description = request.Appointment_Description,
                    CreatedAt = DateTime.UtcNow,
                    Status = Domain.Enums.EntityStatus.Confirmed
                };

                _context.Appointments.Add(appointment);

                // 4️⃣ Mark Slot Unavailable
                timeSlot.IsAvailable = false;

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                // 5️⃣ Fire-and-forget SMS (DO NOT use request cancellation token here)
                _ = Task.Run(async () =>
                {
                    try
                    {
                        var message =
                            $"Hello {patient.PatientName}, " +
                            $"your appointment with Dr {timeSlot.Doctor.DoctorName} " +
                            $"is confirmed for {request.AppointmentDate:dddd, dd MMM yyyy} " +
                            $"at {timeSlot.StartTime:hh\\:mm}.";

                        await _twilioService
                            .SendAppointmentConfirmationAsync(
                                patient.PhoneNumber,
                                message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(
                            ex,
                            "Failed to send SMS confirmation for appointment {AppointmentId}",
                            appointment.Id);
                    }

                }, CancellationToken.None);

                // 6️⃣ Raise Domain Event
                appointment.AddDomainEvent(
                    new AppointmentBookedEvent(appointment));

                return new BookAppointmentResult
                {
                    AppointmentId = appointment.Id,
                    Message = "Appointment booked successfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogError(ex, "Error booking appointment");
                throw;
            }
        }

    }
}


using Application.Interfaces;
using Domain.Common.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Application.Abstractions.Data;

namespace Application.Features.Appointments.Events
{
  

public sealed class AppointmentBookedEventHandler
    : INotificationHandler<AppointmentBookedEvent>
    {
        private readonly ITwilioService _twilioService;
        private readonly IApplicationDbContext _context;
        private readonly ILogger<AppointmentBookedEventHandler> _logger;

        public AppointmentBookedEventHandler(
            ITwilioService twilioService,
            IApplicationDbContext context,
            ILogger<AppointmentBookedEventHandler> logger)
        {
            _twilioService = twilioService;
            _context = context;
            _logger = logger;
        }

        public async Task Handle(
            AppointmentBookedEvent notification,
            CancellationToken cancellationToken)
        {
            try
            {
                var appointment = notification.Appointment;

                var fullData = await _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.TimeSlot)
                        .ThenInclude(ts => ts.Doctor)
                    .FirstAsync(a => a.Id == appointment.Id, cancellationToken);

                var message =
                    $"Hello {fullData.Patient.PatientName}, " +
                    $"your appointment with Dr {fullData.TimeSlot.Doctor.DoctorName} " +
                    $"is confirmed for {fullData.AppointmentDate:dddd, dd MMM yyyy} " +
                    $"at {fullData.TimeSlot.StartTime:hh\\:mm}.";

                await _twilioService.SendAppointmentConfirmationAsync(
                    fullData.Patient.PhoneNumber,
                    message);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error sending SMS for appointment {AppointmentId}",
                    notification.Appointment.Id);
            }
        }
    }

}

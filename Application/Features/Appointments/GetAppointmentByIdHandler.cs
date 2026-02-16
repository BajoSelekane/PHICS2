using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Appointments
{
    public sealed class GetAppointmentByIdHandler(IApplicationDbContext context)
        : IQueryHandler<GetAppointmentByIdQuery, AppointmentDto>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<AppointmentDto> Handle(
            GetAppointmentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments
                .AsNoTracking()
                .FirstAsync(a => a.Id == request.Id, cancellationToken);

            return new AppointmentDto(
                            appointment.Id, 
                appointment.PatientId,
                appointment.TimeSlotId,
                appointment.DoctorId,
                appointment.Doctor.DoctorName,
                appointment.PhoneNumber,
                appointment.AppointmentDate,
                appointment.EndTime,
                appointment.Appointment_Description,
                appointment.Status
            );
        }

        Task<SharedLibrary.Shared.Result<AppointmentDto>> MediatR.IRequestHandler<GetAppointmentByIdQuery, SharedLibrary.Shared.Result<AppointmentDto>>.Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}

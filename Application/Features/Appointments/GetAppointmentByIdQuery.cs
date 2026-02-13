using Application.Abstractions.Messaging;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Features.Appointments
{
    public sealed record GetAppointmentByIdQuery(Guid Id)
      : IQuery<AppointmentDto>;

}

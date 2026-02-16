using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TimeSlots.Query
{
    public record GetAvailableTimeSlotsQuery(Guid DoctorId, DateTime Date)
    : IRequest<List<TimeSlotDto>>;

}

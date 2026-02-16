using Application.DTOs;
using MediatR;

namespace Application.Features.Appointments.Queries
{
    public record GetAvailableTimeSlotsQuery : IRequest<List<TimeSlotDto>>
    {
        public Guid DoctorId { get; init; }
        //public DateTime Date { get; init; }
        public DateOnly Date { get; init; }

        // public DayOfWeek DayOfWeek { get; init; }

    }
}

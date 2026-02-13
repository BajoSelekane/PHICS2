using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record TimeSlotDto
    {
        public Guid Id { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
        public DateTime DayOfWeek { get; set; }
        public bool IsAvailable { get; init; }
    }
}

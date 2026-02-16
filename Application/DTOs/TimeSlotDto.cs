using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record TimeSlotDto(
    Guid Id,
    TimeSpan StartTime,
    TimeSpan EndTime,
    bool IsAvailable);

}

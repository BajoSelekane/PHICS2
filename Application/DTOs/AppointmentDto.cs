using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public sealed record AppointmentDto(
       Guid Id,
       Guid PatientId,
       Guid TimeSlotId,
       Guid? DoctorId,
       string DoctorName,
       string PhoneNumber,
       DateTime AppointmentDate,
       TimeSpan EndTime,
       string? Description,
        Enum Status

   );
}

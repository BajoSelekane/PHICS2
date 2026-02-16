using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record PatientDto(Guid Id, string IDNumber, string PatientName, string? Email, string PhoneNumber, DateTime DateOfBirth, string Appointment_Description);
}

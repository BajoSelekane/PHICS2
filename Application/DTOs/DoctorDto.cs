using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record DoctorDto(Guid Id, string DoctorName, string Specialty, string? Email);
}

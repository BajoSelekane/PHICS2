using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Clinics : BaseEntity
    {
        public required string ClinicName { get; set; }
        public string? Location { get; set; }
        public List<Appointment>? Appointments { get; set; } 
    }
}

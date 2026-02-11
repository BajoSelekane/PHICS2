using Domain.Entities;

namespace Domain
{
    public class Appointment :BaseEntity
    {
        public required string PatientId { get; set; }    
        public Clinics? Clinics { get; set; }
        public string? ClinicsId { get; set; }
        public string? DoctorId { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime AppointmentDate { get; set; } 
        public DateTime EndTime { get; set; }
        public Patient Patient { get; set; } = default!;
        public Doctor Doctor { get; set; } = default!;
        public TimeSlot Timeslot { get; set; } = default!;
        public string? Appointment_Description { get; set; }
        public ICollection<Patient> Patients { get; set; } = [];

        //public bool Status { get; set; } = false;
        //public Guid TimeSlotId { get; set; }
        //public string? ClinicId { get; set; }  

    }
}

using Domain.Entities;

namespace Domain
{
    public class Appointment :BaseEntity
    {
        public Guid PatientId { get; set; }    
        public Clinics? Clinics { get; set; }
        public Guid? ClinicsId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid TimeSlotId { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime AppointmentDate { get; set; } 
        public TimeSpan EndTime { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public string? Appointment_Description { get; set; }
        public virtual Patient Patient { get; set; } = default!;
        public virtual TimeSlot TimeSlot { get; set; } = default!;

        //public TimeSlot Timeslot { get; set; } = default!;
        //public bool Status { get; set; } = false;
        //public Guid TimeSlotId { get; set; }
        //public string? ClinicId { get; set; }  

    }
}

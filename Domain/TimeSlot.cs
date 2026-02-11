using Domain.Entities;

namespace Domain
{
    public class TimeSlot :BaseEntity
    {
        public string AppointmentId { get; set; } = default!;
        public Doctor Doctors { get; set; } = default!;
        public string PatientId { get; set; } = default!;
        public string DoctorId { get; set; } = default!;
        public DateTime DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; } =false;
        public Appointment? Appointments { get; set; }
     

    }
}

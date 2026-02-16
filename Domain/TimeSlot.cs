using Domain;
using Domain.Entities;

namespace Domain
{
    public class TimeSlot :BaseEntity
    {
        //public Doctor Doctors { get; set; } = default!;
        public Guid PatientId { get; set; } = default!;
        public Guid DoctorId { get; set; } = default!;
        public DateOnly DayOfWeek { get; set; }
        public DateOnly SlotDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; } =false;

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Appointment Appointment { get; set; } =null!;
        //public Appointment? Appointments { get; set; }



    }
}


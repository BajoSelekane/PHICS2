namespace Domain.Entities
{
    public class Doctor: BaseEntity
    {
        public required string DoctorName { get; set; }
        public required string Specialty { get; set; }
        public string? Email { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
    }
}

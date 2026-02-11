namespace Domain
{
    public class Patient: BaseEntity
    {
        public required string IDNumber { get; set; }
        public required string PatientName { get; set; }
        public string? Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Appointment_Description { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = [];

    }
}

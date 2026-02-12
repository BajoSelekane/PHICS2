using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PHICS2.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<TimeSlot> Timeslots => Set<TimeSlot>();
        public DbSet<Login> Logins => Set<Login>();
        public DbSet<Register> Registers => Set<Register>();
        public DbSet<Clinics> Clinics => Set<Clinics>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
           .HasOne(a => a.Patient)
           .WithMany(p => p.Appointments)
           .HasForeignKey(a => a.PatientId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasQueryFilter(p => !p.IsDeleted);


            modelBuilder.Entity<Appointment>()
               .HasQueryFilter(c => !c.IsDeleted);


            modelBuilder.Entity<TimeSlot>()
               .HasQueryFilter(t => !t.IsDeleted);


            base.OnModelCreating(modelBuilder);


        }
    }
}
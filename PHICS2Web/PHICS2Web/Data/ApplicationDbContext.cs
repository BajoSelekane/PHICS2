using Domain;
using Domain.Common;
using Domain.Common.Events;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PHICS2Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IMediator? mediator = null)   // inject MediatR
            : base(options)
        {
            _mediator = mediator;
            
        }
    


        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<Login> Logins => Set<Login>();
        public DbSet<Register> Registers => Set<Register>();
        public DbSet<Clinics> Clinics => Set<Clinics>();

        public override async Task<int> SaveChangesAsync(
       CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker
                .Entries<BaseEntityEvenrts>()
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            foreach (var entity in ChangeTracker.Entries<BaseEntityEvenrts>())
            {
                entity.Entity.ClearDomainEvents();
            }

            return result;
        }

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

            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.TimeSlot)
            .WithOne(t => t.Appointment)
            .HasForeignKey<Appointment>(a => a.TimeSlotId)
            .OnDelete(DeleteBehavior.Restrict);
 

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.TimeSlots)
                .WithOne(t => t.Doctor)
                .HasForeignKey(t => t.DoctorId);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);




            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Clinics>()
               .HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<Doctor>()
               .HasQueryFilter(d => !d.IsDeleted);

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
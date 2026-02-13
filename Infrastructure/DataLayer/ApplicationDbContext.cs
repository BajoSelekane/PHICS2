using Application.Abstractions.Data;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataLayer
{
    public class ApplicationDbContext
    : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        // ✅ Regular DbSet properties (public)
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<Clinics> Clinics => Set<Clinics>();


        // ✅ Explicit interface implementations
        DbSet<Patient> IApplicationDbContext.Patients => Patients;
        DbSet<Doctor> IApplicationDbContext.Doctors => Doctors;
        DbSet<Appointment> IApplicationDbContext.Appointments => Appointments;
        DbSet<TimeSlot> IApplicationDbContext.TimeSlots => TimeSlots;
        DbSet<Clinics> IApplicationDbContext.Clinic => Clinics;

        Task<int> IApplicationDbContext.SaveChangesAsync(
            CancellationToken cancellationToken)
            => base.SaveChangesAsync(cancellationToken);


        public async Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken)
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }
    }


}

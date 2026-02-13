using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Abstractions.Data;



public interface IApplicationDbContext
{
    DbSet<Patient> Patients { get; }
    DbSet<Doctor> Doctors { get; }
    DbSet<Appointment> Appointments { get; }
    DbSet<TimeSlot> TimeSlots { get; }
    DbSet<Clinics> Clinic { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken);
}

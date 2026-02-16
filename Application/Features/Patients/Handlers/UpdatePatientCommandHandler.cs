using Application.Abstractions.Data;
using Application.DTOs;
using Application.Features.Patients.Commands;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Patients.Handlers
{
    
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, PatientDto>
    {
        private readonly IApplicationDbContext _context;
        public UpdatePatientCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<PatientDto> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                IDNumber = request.IDNumber,
                PatientName = request.PatientName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Appointment_Description = request.Appointment_Description
            };
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync(cancellationToken);
            return new PatientDto(patient.Id, patient.IDNumber, patient.PatientName, patient.Email, patient.PhoneNumber, patient.DateOfBirth, patient.Appointment_Description);
        }
    }

}

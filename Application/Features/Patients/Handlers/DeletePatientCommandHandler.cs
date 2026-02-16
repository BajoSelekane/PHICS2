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
 
    //public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, PatientDto>
    //{
    //    private readonly IApplicationDbContext _context;
    //    public DeletePatientCommandHandler(IApplicationDbContext context) => _context = context;

    //    public Task<PatientDto> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //          //return new PatientDto(patient.Id, patient.IDNumber, patient.PatientName, patient.Email, patient.PhoneNumber, patient.DateOfBirth, patient.Appointment_Description);
    //    }
    //}

}

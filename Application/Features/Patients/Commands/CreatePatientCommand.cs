using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Patients.Commands
{
    public record CreatePatientCommand(string IDNumber, string PatientName, string? Email, string PhoneNumber, DateTime DateOfBirth, string Appointment_Description) : IRequest<PatientDto>;
}

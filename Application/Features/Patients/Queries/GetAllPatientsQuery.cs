using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Patients.Queries
{
    public record GetAllPatientsQuery() : IRequest<List<PatientDto>>;
}

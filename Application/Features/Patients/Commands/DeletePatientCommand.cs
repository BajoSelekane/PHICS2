using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Patients.Commands
{
    public record DeletePatientCommand(Guid Id) : IRequest;
}

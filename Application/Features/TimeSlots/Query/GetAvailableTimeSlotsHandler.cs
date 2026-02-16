using Application.Abstractions.Data;
using Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TimeSlots.Query
{
    public class GetAvailableTimeSlotsHandler
      : IRequestHandler<GetAvailableTimeSlotsQuery, List<TimeSlotDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAvailableTimeSlotsHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeSlotDto>> Handle(
            GetAvailableTimeSlotsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.TimeSlots
                .Where(x => x.DoctorId == request.DoctorId
                         && x.IsAvailable)
              .Select(x => new TimeSlotDto(
                    x.Id,
                    x.StartTime,
                    x.EndTime,
                    x.IsAvailable))
                .ToListAsync(cancellationToken);
        }
    }

}

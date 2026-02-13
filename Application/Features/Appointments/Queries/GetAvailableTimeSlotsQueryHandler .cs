using Application.Abstractions.Data;
using Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Appointments.Queries
{
    public class GetAvailableTimeSlotsQueryHandler : IRequestHandler<GetAvailableTimeSlotsQuery, List<TimeSlotDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAvailableTimeSlotsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeSlotDto>> Handle(GetAvailableTimeSlotsQuery request,
             CancellationToken cancellationToken)
        {
            var dayOfWeek = request.Date.DayOfWeek;

            return await _context.TimeSlots
                .Where(ts =>
                    ts.DoctorId == request.DoctorId &&
                    ts.SlotDate == request.Date &&
                    ts.IsAvailable)
                .OrderBy(ts => ts.StartTime)
                .Select(ts => new TimeSlotDto
                {
                    Id = ts.Id,
                    StartTime = ts.StartTime,
                    EndTime = ts.EndTime,
                    IsAvailable = ts.IsAvailable
        })
        .ToListAsync(cancellationToken);
        }


        //public async Task<List<TimeSlotDto>> Handle(GetAvailableTimeSlotsQuery request, CancellationToken cancellationToken)
        //{
        //   // var dayOfWeek = request.Date.DayOfWeek;
        //    DateTime dayOfWeek = request.Date.ParseExact(DayOfWeek, "yyyyMMddhhmm", null);

        //    return await _context.TimeSlots
        //        .Where(ts => ts.DoctorId == request.DoctorId
        //            && ts.DayOfWeek == dayOfWeek
        //            && ts.IsAvailable)
        //        .OrderBy(ts => ts.StartTime)
        //        .Select(ts => new TimeSlotDto
        //        {
        //            Id = ts.Id,
        //            StartTime = ts.StartTime,
        //            EndTime = ts.EndTime,
        //            IsAvailable = ts.IsAvailable
        //        })
        //        .ToListAsync(cancellationToken);
        //}
    }
}

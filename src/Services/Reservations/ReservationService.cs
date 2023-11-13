using Foodtruck.Persistence;
using Foodtruck.Shared.Reservations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Reservations;

public class ReservationService : IReservationService
{
    private readonly FoodtruckDbContext dbContext;

    public ReservationService(FoodtruckDbContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task<ReservationResult.Index> GetIndexAsync(ReservationRequest.Index request)
    {
        var query = dbContext.Reservations.AsQueryable();

        query = query.Where(x => request.Statuses.Select(x => (int)x).Contains((int)x.Status));
        query = query.Where(x => x.Start >= request.FromDate.Date);

        if (request.ToDate is not null)
        {
            query = query.Where(x => x.Start <= request.ToDate.Value.Date);
        }

        int totalAmount = await query.CountAsync();

        var items = await query
          .Skip((request.Page - 1) * request.PageSize)
          .Take(request.PageSize)
          .OrderBy(x => x.Id)
          .Select(x => new ReservationDto.Index
          {
              Id = x.Id,
              Start = x.Start,
              End = x.End,
              Status = (StatusDto)((int)x.Status),
          }).ToListAsync();

        var result = new ReservationResult.Index
        {
            Reservations = items,
            TotalAmount = totalAmount
        };
        return result;
    }

    public async Task<ReservationResult.Detailed> GetDetailedAsync(ReservationRequest.Detailed request)
    {
        var query = dbContext.Reservations.AsQueryable();

        query = query.Where(x => request.Statuses.Select(x => (int)x).Contains((int)x.Status));
        query = query.Where(x => x.Start >= request.FromDate.Date);

        if (request.ToDate is not null)
        {
            query = query.Where(x => x.Start <= request.ToDate.Value.Date);
        }

        int totalAmount = await query.CountAsync();

        var items = await query
          .Skip((request.Page - 1) * request.PageSize)
          .Take(request.PageSize)
          .OrderBy(x => x.Id)
          .Select(x => new ReservationDto.Detail
          {
              Id = x.Id,
              Start = x.Start,
              End = x.End,
              Description = x.Description,
              Status = (StatusDto)((int)x.Status),
          }).ToListAsync();

        var result = new ReservationResult.Detailed
        {
            Reservations = items,
            TotalAmount = totalAmount
        };
        return result;
    }
}

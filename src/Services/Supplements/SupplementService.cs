using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodtruck.Shared.Supplements;
using Domain;
using Foodtruck.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Services.Supplements;
public class SupplementService : ISupplementService
{

    private readonly BogusDbContext dbContext;

    public SupplementService(BogusDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(SupplementDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int supplementId)
    {
        throw new NotImplementedException();
    }

    public async Task EditAsync(int supplementId, SupplementDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public async Task<SupplementDto.Detail> GetDetailAsync(int supplementId)
    {
        throw new NotImplementedException();
    }

    public async Task<SupplementResult.Index> GetIndexAsync(SupplementRequest.Index request)
    {
        var query = dbContext.Supplements.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(s => s.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        if (request.MinPrice is not null)
        {
            query = query.Where(x => x.Price.Value >= request.MinPrice);
        }

        if (request.MaxPrice is not null)
        {
            query = query.Where(x => x.Price.Value <= request.MaxPrice);
        }

        if (request.MinAvailableAmount is not null)
        {
            query = query.Where(x => x.AmountAvailable >= request.MinAvailableAmount);
        }

        if (request.MaxAvailableAmount is not null)
        {
            query = query.Where(x => x.AmountAvailable <= request.MaxAvailableAmount);
        }

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(s => s.Category.Equals(request.Category, StringComparison.OrdinalIgnoreCase));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new SupplementDto.Index
           {
               Id = x.Id,
               Name = x.Name,
           }).ToListAsync();

        var result = new SupplementResult.Index
        {
            Supplements = items,
            TotalAmount = totalAmount
        };
        return result;
    }
}

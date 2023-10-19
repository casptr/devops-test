using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodtruck.Shared.Supplements;
using Domain;
using Foodtruck.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;
using Bogus;
using Domain.Common;
using Foodtruck.Shared.Formulas;

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
        if (await dbContext.Supplements.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(Supplement), nameof(Supplement.Name), model.Name);

        Faker imageFaker = new();

        Money price = new(model.Price);
        Supplement supplement = new(model.Name, model.Description, model.Category, price, imageFaker.Image.PicsumUrl(), model.AmountAvailable);

        dbContext.Supplements.Add(supplement);
        await dbContext.SaveChangesAsync();

        return supplement.Id;
    }

    public async Task DeleteAsync(int supplementId)
    {
        Supplement? supplement = await dbContext.Supplements.SingleOrDefaultAsync(x => x.Id == supplementId);

        if (supplement is null)
            throw new EntityNotFoundException(nameof(Supplement), supplementId);

        dbContext.Supplements.Remove(supplement);

        await dbContext.SaveChangesAsync();
    }

    public async Task EditAsync(int supplementId, SupplementDto.Mutate model)
    {
        Supplement? supplement = await dbContext.Supplements.SingleOrDefaultAsync(x => x.Id == supplementId);

        if (supplement is null)
            throw new EntityNotFoundException(nameof(Supplement), supplementId);

        Money price = new(model.Price);
        supplement.Name = model.Name!;
        supplement.Description = model.Description!;
        supplement.Category = model.Category!;
        supplement.Price = price;
        supplement.ImageUrl = model.ImageUrl!;
        supplement.AmountAvailable = model.AmountAvailable;

        await dbContext.SaveChangesAsync();
    }

    public async Task<SupplementResult.Index> GetAllAsync()
    {
        var query = dbContext.Supplements.AsQueryable();
        query = dbContext.Supplements;
        int totalAmount = await query.CountAsync();
       
        var items = await query
          .OrderBy(x => x.Id)
          .Select(x => new SupplementDto.Detail
          {
              Id = x.Id,
              Name = x.Name,
              Price = x.Price.Value,
              Description = x.Description,
              Category = x.Category,
              ImageUrl = x.ImageUrl,
              AmountAvailable = x.AmountAvailable,
              CreatedAt = x.CreatedAt,
              UpdatedAt = x.UpdatedAt
          }).ToListAsync();
        var result = new SupplementResult.Index
        {
            Supplements = items,
            TotalAmount = totalAmount
        };
        return result;
    }


    public async Task<SupplementDto.Detail> GetDetailAsync(int supplementId)
    {
        SupplementDto.Detail? supplement = await dbContext.Supplements.Select(x => new SupplementDto.Detail
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price.Value,
            Description = x.Description,
            Category = x.Category,
            ImageUrl = x.ImageUrl,
            AmountAvailable = x.AmountAvailable,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).SingleOrDefaultAsync(x => x.Id == supplementId);

        if (supplement is null)
            throw new EntityNotFoundException(nameof(Supplement), supplementId);

        return supplement;
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
           .Select(x => new SupplementDto.Detail
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

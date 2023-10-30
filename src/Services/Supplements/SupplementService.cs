using System.Linq;
using Bogus;
using Domain.Common;
using Domain.Exceptions;
using Domain.Supplements;
using Foodtruck.Persistence;
using Foodtruck.Shared.Supplements;
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
        if (await dbContext.Supplements.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(Supplement), nameof(Supplement.Name), model.Name);

        Faker imageFaker = new();

        Money price = new(model.Price);
        Supplement supplement = new(model.Name, model.Description, model.Category, price, model.AmountAvailable);

        supplement.AddImageUrl(new Uri(imageFaker.Image.PicsumUrl()));
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
        supplement.AmountAvailable = model.AmountAvailable;

		//TODO: edit images
		//supplement.ImageUrls = model.ImageUrls;

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
              Category = new CategoryDto.Index { Name = x.Category.Name },
              ImageUrls = x.ImageUrls.ToList(), 
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
            Category = new CategoryDto.Index { Name = x.Category.Name },
            ImageUrls = x.ImageUrls,
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
            query = query.Where(s => s.Category.Name.Equals(request.Category, StringComparison.OrdinalIgnoreCase));
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
    //image test
    public async Task AddImage(int supplementId)
    {
        Supplement? supplement = await dbContext.Supplements.SingleOrDefaultAsync(x => x.Id == supplementId);
        if (supplement is null)
            throw new EntityNotFoundException(nameof(Supplement), supplementId);
        Faker faker = new();
        var image = new Uri(faker.Image.PicsumUrl());
        supplement.AddImageUrl(image);
        dbContext.Entry(supplement).State = EntityState.Modified;
        int update = await dbContext.SaveChangesAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Domain.Common;
using Domain.Exceptions;
using Foodtruck.Persistence;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Microsoft.EntityFrameworkCore;

namespace Services.Formulas;
public class FormulaService : IFormulaService
{
    private readonly BogusDbContext dbContext;

    public FormulaService(BogusDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(FormulaDto.Mutate model)
    {
        if (await dbContext.Formulas.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(Formula), nameof(Formula.Name), model.Name);

        Faker imageFaker = new();

        Money price = new(model.Price);
        Formula formula = new(model.Name, model.Description, price, imageFaker.Image.PicsumUrl());

        dbContext.Formulas.Add(formula);
        await dbContext.SaveChangesAsync();

        return formula.Id;
    }

    public async Task DeleteAsync(int formulaId)
    {
        Formula? formula = await dbContext.Formulas.SingleOrDefaultAsync(x => x.Id == formulaId);

        if (formula is null)
            throw new EntityNotFoundException(nameof(Formula), formulaId);

        dbContext.Formulas.Remove(formula);

        await dbContext.SaveChangesAsync();
    }

    public async Task EditAsync(int formulaId, FormulaDto.Mutate model)
    {
        Formula? formula = await dbContext.Formulas.SingleOrDefaultAsync(x => x.Id == formulaId);

        if (formula is null)
            throw new EntityNotFoundException(nameof(Formula), formulaId);

        Money price = new(model.Price);
        formula.Name = model.Name!;
        formula.Description = model.Description!;
        formula.Price = price;
        formula.ImageUrl = model.ImageUrl!;
        //formula.IncludedSupplements = model.IncludedSupplements!;

        await dbContext.SaveChangesAsync();
    }


    public async Task<FormulaDto.Detail> GetDetailAsync(int formulaId)
    {
        FormulaDto.Detail? formula = await dbContext.Formulas.Select(x => new FormulaDto.Detail
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price.Value,
            Description = x.Description,
            IncludedSupplements = x.IncludedSupplements.Select(x => x.Name),
            ImageUrl = x.ImageUrl,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).SingleOrDefaultAsync(x => x.Id == formulaId);

        if (formula is null)
            throw new EntityNotFoundException(nameof(Formula), formulaId);

        return formula;
    }

    public async Task<FormulaResult.Index> GetAllAsync()
    {
        var query = dbContext.Formulas.AsQueryable();
        query = dbContext.Formulas;
        int totalAmount = await query.CountAsync();

        var items = await query
           .OrderBy(x => x.Id)
           .Select(x => new FormulaDto.Detail
           {
               Id = x.Id,
               Name = x.Name,
               Price = x.Price.Value,
               Description = x.Description,
               IncludedSupplements = x.IncludedSupplements.Select(x => x.Name),
               ImageUrl = x.ImageUrl,
               CreatedAt = x.CreatedAt,
               UpdatedAt = x.UpdatedAt
           }).ToListAsync();

        var result = new FormulaResult.Index
        {
            Formulas = items,
            TotalAmount = totalAmount
        };
        return result;
    }

    // TODO: Admin add supplement to formula
    /*public async Task AddTagAsync(int productId, int tagId)
    {
        Product? product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product), productId);

        Tag? tag = await dbContext.Tags.SingleOrDefaultAsync(x => x.Id == tagId);

        if (tag is null)
            throw new EntityNotFoundException(nameof(Tag), tagId);

        product.Tag(tag);

        await dbContext.SaveChangesAsync();
    }*/
}

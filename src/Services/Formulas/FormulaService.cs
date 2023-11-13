using Bogus;
using Domain.Common;
using Domain.Exceptions;
using Domain.Formulas;
using Fakers.Formulas;
using Foodtruck.Persistence;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Services.Formulas;
public class FormulaService : IFormulaService
{
    private readonly FoodtruckDbContext dbContext;

    private static int sid = 1000;

    public FormulaService(FoodtruckDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<FormulaResult.Index> GetAllAsync()
    {
        var query = dbContext.Formulas.AsQueryable();
        query = dbContext.Formulas;
        int totalAmount = await query.CountAsync();
        var items = await query
           .OrderBy(x => x.Id).Select(x => new FormulaDto.Detail
           {
               Id = x.Id,
               Title = x.Title,
               Description = x.Description,
               IncludedSupplements = x.IncludedSupplements.Select(x => new FormulaSupplementLineDto.Detail
               {
                   Quantity = x.Quantity,
                   Id = x.Id,
                   Supplement = new SupplementDto.Detail
                   {
                       Id = x.Supplement.Id,
                       Name = x.Supplement.Name,
                       Category = new CategoryDto.Index { Id = x.Supplement.Category.Id, Name = x.Supplement.Category.Name },
                       AmountAvailable = x.Supplement.AmountAvailable,
                       Description = x.Supplement.Description,
                       Price = x.Supplement.Price.Value,
                       CreatedAt = x.Supplement.CreatedAt,
                       UpdatedAt = x.Supplement.UpdatedAt,
                       ImageUrls = x.Supplement.ImageUrls.Select(image => image.Image)!
                   }
               }),

               Choices = x.Choices.Select(x => new FormulaSupplementChoiceDto.Detail
               {
                   Id = x.Id,
                   DefaultChoice = new SupplementDto.Detail
                   {
                       Id = x.DefaultChoice.Id,
                       Name = x.DefaultChoice.Name,
                       Category = new CategoryDto.Index { Id = x.DefaultChoice.Category.Id, Name = x.DefaultChoice.Category.Name },
                       AmountAvailable = x.DefaultChoice.AmountAvailable,
                       Description = x.DefaultChoice.Description,
                       Price = x.DefaultChoice.Price.Value,
                       CreatedAt = x.DefaultChoice.CreatedAt,
                       UpdatedAt = x.DefaultChoice.UpdatedAt,
                       ImageUrls = x.DefaultChoice.ImageUrls.Select(image => image.Image)!
                   },

                   MinQuantity = x.MinQuantity,
                   Name = x.Name,
                   SupplementsToChoose = x.SupplementsToChoose.Select(x => new SupplementDto.Detail
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Category = new CategoryDto.Index { Id = x.Id, Name = x.Category.Name },
                       AmountAvailable = x.AmountAvailable,
                       Description = x.Description,
                       Price = x.Price.Value,
                       CreatedAt = x.CreatedAt,
                       UpdatedAt = x.UpdatedAt,
                       ImageUrls = x.ImageUrls.Select(image => image.Image)!
                   })
               }),

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

    public async Task<FormulaDto.Detail> GetDetailAsync(int formulaId)
    {
        FormulaDto.Detail? formula = await dbContext.Formulas.Select(x => new FormulaDto.Detail
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            IncludedSupplements = x.IncludedSupplements.Select(x => new FormulaSupplementLineDto.Detail
            {
                Quantity = x.Quantity,
                Id = x.Id,
                Supplement = new SupplementDto.Detail
                {
                    Id = x.Supplement.Id,
                    Name = x.Supplement.Name,
                    Category = new CategoryDto.Index { Id = x.Supplement.Category.Id, Name = x.Supplement.Category.Name },
                    AmountAvailable = x.Supplement.AmountAvailable,
                    Description = x.Supplement.Description,
                    Price = x.Supplement.Price.Value,
                    CreatedAt = x.Supplement.CreatedAt,
                    UpdatedAt = x.Supplement.UpdatedAt,
                    ImageUrls = x.Supplement.ImageUrls.Select(image => image.Image)!
                }
            }),

            Choices = x.Choices.Select(x => new FormulaSupplementChoiceDto.Detail
            {
                Id = x.Id,
                DefaultChoice = new SupplementDto.Detail
                {
                    Id = x.DefaultChoice.Id,
                    Name = x.DefaultChoice.Name,
                    Category = new CategoryDto.Index { Id = x.DefaultChoice.Category.Id, Name = x.DefaultChoice.Category.Name },
                    AmountAvailable = x.DefaultChoice.AmountAvailable,
                    Description = x.DefaultChoice.Description,
                    Price = x.DefaultChoice.Price.Value,
                    CreatedAt = x.DefaultChoice.CreatedAt,
                    UpdatedAt = x.DefaultChoice.UpdatedAt,
                    ImageUrls = x.DefaultChoice.ImageUrls.Select(image => image.Image)!
                },
                MinQuantity = x.MinQuantity,
                Name = x.Name,
                SupplementsToChoose = x.SupplementsToChoose.Select(x => new SupplementDto.Detail
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = new CategoryDto.Index { Id = x.Category.Id, Name = x.Category.Name },
                    AmountAvailable = x.AmountAvailable,
                    Description = x.Description,
                    Price = x.Price.Value,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    ImageUrls = x.ImageUrls.Select(image => image.Image)!
                })
            }),
            ImageUrl = x.ImageUrl,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).SingleOrDefaultAsync(x => x.Id == formulaId);

        if (formula is null)
            throw new EntityNotFoundException(nameof(Formula), formulaId);

        return formula;
    }


    //public async Task<int> CreateAsync(FormulaDto.Mutate model)
    //{
    //    if (await dbContext.Formulas.AnyAsync(x => x.Title == model.Title))
    //        throw new EntityAlreadyExistsException(nameof(Formula), nameof(Formula.Title), model.Title);

    //    Faker imageFaker = new();
    //    FoodtruckFaker f = new FoodtruckFaker();
    //    Formula formula = new(f.Generate(1).First(), model.Title, model.Description, new Uri(imageFaker.Image.PicsumUrl()));

    //    dbContext.Formulas.Add(formula);
    //    await dbContext.SaveChangesAsync();

    //    return formula.Id;
    //}

    //public async Task DeleteAsync(int formulaId)
    //{
    //    Formula? formula = await dbContext.Formulas.SingleOrDefaultAsync(x => x.Id == formulaId);

    //    if (formula is null)
    //        throw new EntityNotFoundException(nameof(Formula), formulaId);

    //    dbContext.Formulas.Remove(formula);

    //    await dbContext.SaveChangesAsync();
    //}

    //public async Task EditAsync(int formulaId, FormulaDto.Mutate model)
    //{
    //    Formula? formula = await dbContext.Formulas.SingleOrDefaultAsync(x => x.Id == formulaId);

    //    if (formula is null)
    //        throw new EntityNotFoundException(nameof(Formula), formulaId);

    //    formula.Title = model.Title!;
    //    formula.Description = model.Description!;
    //    formula.ImageUrl = model.ImageUrl!;
    //    //formula.IncludedSupplements = model.IncludedSupplements!;

    //    await dbContext.SaveChangesAsync();
    //}


    //private decimal CalculateMinFormulaPrice(int numberOfDays, Formula formula)
    //{
    //    return formula.Foodtruck.CalculatePrice(numberOfDays).Value
    //         + formula.IncludedSupplements.Aggregate(0M, (sum, line) => sum + (line.Quantity + 0.0M) * line.Supplement.Price.Value)
    //         + formula.Choices.Aggregate(0M, (sum, choice) => sum + (choice.MinQuantity + 0.0M) * (choice.SupplementsToChoose.OrderBy(supplement => supplement.Price.Value).First().Price.Value));
    //}


    //public async Task AddFormulaSupplementLine(int formulaId)
    //{
    //    Formula? formula = await dbContext.Formulas.SingleOrDefaultAsync(x => x.Id == formulaId);
    //    if (formula is null)
    //        throw new EntityNotFoundException(nameof(Formula), formulaId);
    //    FormulaSupplementLineFaker faker = new();

    //    faker.RuleFor(x => x.Id, f => sid++);
    //    formula.AddIncludedSupplementLine(faker.Generate(1).First());
    //    dbContext.Entry(formula).State = EntityState.Modified;
    //    await dbContext.SaveChangesAsync();
    //}
    //public async Task AddFormulaSupplementChoice(int formulaId)
    //{
    //    Formula? formula = await dbContext.Formulas.SingleOrDefaultAsync(x => x.Id == formulaId);
    //    if (formula is null)
    //        throw new EntityNotFoundException(nameof(Formula), formulaId);
    //    FormulaSupplementChoiceFaker faker = new();

    //    faker.RuleFor(x => x.Id, f => sid++);
    //    formula.AddSupplemenChoice(faker.Generate(1).First());
    //    dbContext.Entry(formula).State = EntityState.Modified;
    //    await dbContext.SaveChangesAsync();
    //}


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

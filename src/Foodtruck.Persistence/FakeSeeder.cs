using Bogus;
using EntityFrameworkCore.Triggered.Extensions;
using Fakers.Customers;
using Fakers.Formulas;
using Fakers.Quotations;
using Fakers.Supplements;
using Microsoft.EntityFrameworkCore;

namespace Foodtruck.Persistence;

public class FakeSeeder
{
    private readonly BogusDbContext dbContext;
   
    public FakeSeeder(BogusDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
		SeedCustomers();
        SeedReservation();
        SeedCategories();
       
        SeedPricePerDayLines();
        SeedFoodtruck();
        SeedSupplements();
        SeedFormulas();
		SeedFormulaSupplementChoices();
		SeedFormulaSupplementLines();
        SeedQuotationSupplementLines();
		SeedQuotation();
	}

   

    private void SeedQuotation()
	{
		var quotation = new QuotationFaker().AsTransient().UseSeed(101).Generate(1).First();
		var quotationVersion = new QuotationVersionFaker().AsTransient().UseSeed(101).Generate(1).First();

		quotation.AddVersion(quotationVersion);

		dbContext.QuotationVersions.AddRange(quotationVersion);
        dbContext.Quotations.AddRange(quotation);
		dbContext.SaveChanges();
	}

	private void SeedQuotationSupplementLines()
	{
		var quotationSupplementLines = new QuotationSupplementLineFaker().AsTransient().UseSeed(101).Generate(3);

		dbContext.QuotationSupplementLines.AddRange(quotationSupplementLines);
		dbContext.SaveChanges();
	}

	private void SeedFormulaSupplementChoices()
	{
		var formulaSupplementChoices = new FormulaSupplementChoiceFaker().AsTransient().UseSeed(101).Generate(3);

		dbContext.FormulaSupplementChoices.AddRange(formulaSupplementChoices);
		dbContext.SaveChanges();
	}

    private void SeedFormulaSupplementLines()
    {
        var formulaSupplementLines = new FormulaSupplementLineFaker().AsTransient().UseSeed(101).Generate(3);

        dbContext.FormulaSupplementLines.AddRange(formulaSupplementLines);
        dbContext.SaveChanges();
    }

    private void SeedFoodtruck()
	{
		var foodtruck = new FoodtruckFaker().AsTransient().UseSeed(101).Generate(1);

		dbContext.Foodtruck.AddRange(foodtruck);
		dbContext.SaveChanges();
	}

	private void SeedReservation()
	{
		var reservations = new ReservationFaker().AsTransient().UseSeed(101).Generate(3);

		dbContext.Reservations.AddRange(reservations);
		dbContext.SaveChanges();
	}

	private void SeedPricePerDayLines()
	{
		var pricePerDayLines = new PricePerDayLineFaker().AsTransient().UseSeed(101).Generate(1);

		dbContext.PricePerDayLines.AddRange(pricePerDayLines);
		dbContext.SaveChanges();
	}

	private void SeedCustomers()
    {
        var customers = new CustomerFaker().AsTransient().UseSeed(123).Generate(3);

		dbContext.Customers.AddRange(customers);
		dbContext.SaveChanges();
	}

    private void SeedSupplements()
    {
		var supplements = new SupplementFaker().AsTransient().UseSeed(101).Generate(6);
		dbContext.Supplements.AddRange(supplements);
        dbContext.SaveChanges();

    }

	private void SeedCategories()
	{
        var category = new CategoryFaker().AsTransient().UseSeed(100).Generate(3);

        dbContext.Category.AddRange(category);
        dbContext.SaveChanges();
    }

    private void SeedFormulas()
    {
        var formulas = new FormulaFaker().AsTransient().UseSeed(109).Generate(1);

        dbContext.Formulas.AddRange(formulas);
        dbContext.SaveChanges();
    }

    
}
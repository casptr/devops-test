using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Supplements;
using Fakers.Common;
using Fakers.Customers;
using Fakers.Formulas;
using Fakers.Supplements;
using QuotationTest;

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
		SeedSupplements();
        SeedFormulas();
    }

    private void SeedCustomers()
    {
        var customers = new CustomerFaker().AsTransient().UseSeed(101).Generate(3);

		dbContext.Customers.AddRange(customers);
		dbContext.SaveChanges();
	}

    private void SeedSupplements()
    {
        var fakeimage = new Bogus.Faker();

		var supplements = new SupplementFaker().AsTransient().UseSeed(101).Generate(6);

        foreach(var supplement in supplements)
        {
            supplement.AddImageUrl(fakeimage.Image.PicsumUrl());
        }
        dbContext.Supplements.AddRange(supplements);
        dbContext.SaveChanges();
    }

    private void SeedFormulas()
    {
        int quantity = 0;
        var formulas = new FormulaFaker().AsTransient().UseSeed(109).Generate(3);
        formulas.ForEach(f => {

            var supplements = new FormulaSupplementLineFaker().AsTransient().UseSeed(101).Generate(quantity+=2);
            foreach (var item in supplements)
            {
                f.AddIncludedSupplementLine(item);
            }
        });
        dbContext.Formulas.AddRange(formulas);
        dbContext.SaveChanges();
    }

    
}
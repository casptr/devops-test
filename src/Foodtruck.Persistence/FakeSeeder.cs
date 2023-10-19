using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Fakers.Formulas;
using Fakers.Supplements;

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
        SeedSupplements();
        SeedFormulas();
    }
    private void SeedSupplements()
    {
        var supplements = new SupplementFaker().AsTransient().UseSeed(101).Generate(14);
        dbContext.Supplements.AddRange(supplements);
        dbContext.SaveChanges();
    }

    private void SeedFormulas()
    {
        Random random = new Random();
       
        var formulas = new FormulaFaker().AsTransient().UseSeed(102).Generate(3);
        formulas.ForEach(f => {
           
            var supplements = new SupplementFaker().AsTransient().UseSeed(101).Generate(14).Take(random.Next(1, 15));
            foreach (var item in supplements)
            {
                f.AddIncludedSupplement(item);
            }
        });
        dbContext.Formulas.AddRange(formulas);
        dbContext.SaveChanges();
    }

    
}
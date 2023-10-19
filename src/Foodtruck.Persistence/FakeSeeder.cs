using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        var formulas = new FormulaFaker().AsTransient().UseSeed(101).Generate(3);
        dbContext.Formulas.AddRange(formulas);
        dbContext.SaveChanges();
    }

    
}
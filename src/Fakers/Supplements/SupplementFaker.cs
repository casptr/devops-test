using Domain.Supplements;
using Fakers.Common;

namespace Fakers.Supplements;

public class SupplementFaker : EntityFaker<Supplement>
{
    private static int id = 100;
    public SupplementFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Supplement(f.Commerce.Product(), f.Commerce.ProductName(), new CategoryFaker(), new MoneyFaker(locale), f.Random.Int(0, 101))).RuleFor(f => f.Id, s => id++);
    }
}

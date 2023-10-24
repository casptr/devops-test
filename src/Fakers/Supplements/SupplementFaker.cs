using Domain.Supplements;
using Fakers.Common;

namespace Fakers.Supplements;

public class SupplementFaker : EntityFaker<Supplement>
{
    public SupplementFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Supplement(f.Commerce.Product(), f.Commerce.ProductName(), f.Commerce.ProductMaterial(), new MoneyFaker(locale), f.Random.Int(0, 101)));
    }
}

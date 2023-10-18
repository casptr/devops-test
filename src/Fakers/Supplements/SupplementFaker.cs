using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Fakers.Common;

namespace Fakers.Supplements;

public class SupplementFaker : EntityFaker<Supplement>
{
    public SupplementFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Supplement(f.Commerce.ProductName(), f.Commerce.ProductDescription(), f.Commerce.ProductMaterial(), new MoneyFaker(locale), f.Image.PicsumUrl(), f.Random.Int(0, 101)));
    }
}

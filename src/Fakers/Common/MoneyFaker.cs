using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Domain.Common;

namespace Fakers.Common;

public class MoneyFaker : Faker<Money>
{
    public MoneyFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Money(Math.Round(f.Random.Decimal(1, 1_000), 2)));
    }
}

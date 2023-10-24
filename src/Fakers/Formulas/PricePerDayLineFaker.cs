using Domain.Formulas;
using Fakers.Common;

namespace Fakers.Formulas;

public class PricePerDayLineFaker : EntityFaker<PricePerDayLine>
{
	public PricePerDayLineFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new PricePerDayLine(f.Random.Int(1, 10), new MoneyFaker()));
	}
}

using Domain.Formulas;
using Fakers.Common;
using Fakers.Supplements;

namespace Fakers.Formulas;

public class FormulaSupplementChoiceFaker : EntityFaker<FormulaSupplementChoice>
{
	public FormulaSupplementChoiceFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new FormulaSupplementChoice(f.Commerce.ProductName(), f.Random.Int(1,10), new SupplementFaker()));
	}
}

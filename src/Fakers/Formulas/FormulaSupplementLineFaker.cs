using Domain.Formulas;
using Fakers.Common;
using Domain.Supplements;
using Fakers.Supplements;

namespace Fakers.Formulas;

public class FormulaSupplementLineFaker : EntityFaker<FormulaSupplementLine>
{
	public FormulaSupplementLineFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new FormulaSupplementLine(new FormulaFaker(), new SupplementItemFaker()));
	}
}

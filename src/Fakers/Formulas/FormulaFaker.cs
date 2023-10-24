using Domain.Formulas;
using Fakers.Common;

namespace Fakers.Formulas;

public class FormulaFaker : EntityFaker<Formula>
{
    public FormulaFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Formula(f.Commerce.ProductName(), f.Commerce.ProductDescription(), new MoneyFaker(locale), f.Image.PicsumUrl()));
    }
}

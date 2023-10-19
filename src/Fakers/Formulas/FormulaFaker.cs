using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Fakers.Common;

namespace Fakers.Formulas;

public class FormulaFaker : EntityFaker<Formula>
{
    public FormulaFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Formula(f.Commerce.ProductName(), f.Commerce.ProductDescription(), new MoneyFaker(locale), f.Image.PicsumUrl()));
    }
}

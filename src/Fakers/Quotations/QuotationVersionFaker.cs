using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Quotations;
using Domain.Supplements;
using Fakers.Common;
using Fakers.Customers;
using Fakers.Formulas;
using Fakers.Supplements;

namespace Fakers.Quotations;

public class QuotationVersionFaker : EntityFaker<QuotationVersion>
{
    public static int id = 100;
    public QuotationVersionFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new QuotationVersion(f.Random.Int(10,100), f.Company.Bs(), f.Commerce.ProductDescription(), new ReservationFaker(), new FormulaFaker(),f.Make(3,()=> (SupplementItem)new SupplementItemFaker()).ToList() , new AddressFaker(), new AddressFaker())).RuleFor(f => f.Id, s => id++);
    }
}

using Domain.Quotations;
using Domain.Supplements;
using Fakers.Common;
using Fakers.Customers;
using Fakers.Formulas;
using Fakers.Supplements;

namespace Fakers.Quotations;

public class QuotationFaker : EntityFaker<Quotation>
{
	public QuotationFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Quotation(f.Random.Int(10, 100), f.Company.CatchPhrase(), f.Commerce.ProductDescription(), new ReservationFaker(),
												new FormulaFaker(), new SupplementItemFaker().Generate(f.Random.Int(1, 5)),
												new AddressFaker(), new AddressFaker(), new CustomerFaker()));
	}
}

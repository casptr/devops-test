using Domain.Quotations;
using Fakers.Common;
using Fakers.Customers;

namespace Fakers.Quotations;

public class QuotationFaker : EntityFaker<Quotation>
{
	public QuotationFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Quotation(new CustomerFaker()));
	}
}

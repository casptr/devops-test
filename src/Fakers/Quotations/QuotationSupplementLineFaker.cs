using Domain.Quotations;
using Fakers.Common;
using Fakers.Supplements;

namespace Fakers.Quotations;

public class QuotationSupplementLineFaker : EntityFaker<QuotationSupplementLine>
{
	public QuotationSupplementLineFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new QuotationSupplementLine(new SupplementItemFaker()));
	}
}

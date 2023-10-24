using Bogus;
using Domain.Quotations;
using Fakers.Supplements;

namespace Fakers.Quotations;

public class QuotationSupplementLineFaker : Faker<QuotationSupplementLine>
{
	public QuotationSupplementLineFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new QuotationSupplementLine(new SupplementItemFaker(), f.Random.Bool()));
	}
}

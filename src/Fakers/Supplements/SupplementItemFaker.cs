using Bogus;
using Domain.Supplements;
using Fakers.Common;

namespace Fakers.Supplements;

public class SupplementItemFaker : Faker<SupplementItem>
{
	
	public SupplementItemFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new SupplementItem(new Supplement("test", "test", new CategoryFaker(), new MoneyFaker(), 10), f.Random.Int(1, 150)));
	}
}

using Bogus;
using Domain.Supplements;

namespace Fakers.Supplements;

public class SupplementItemFaker : Faker<SupplementItem>
{
	public SupplementItemFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new SupplementItem(new SupplementFaker(), f.Random.Int(1, 150)));
	}
}

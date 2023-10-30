using Bogus;
using Domain.Customers;

namespace Fakers.Customers;

public class AddressFaker : Faker<Address>
{
	public AddressFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Address(f.Address.ZipCode(), f.Address.City(), f.Address.StreetName(), f.Address.BuildingNumber()));
	}
}

using Domain.Customers;
using Fakers.Common;

namespace Fakers.Customers;

public class CustomerFaker : EntityFaker<Customer>
{
	public CustomerFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Customer(f.Person.FirstName, f.Person.LastName, new EmailAddress(f.Person.Email), f.Person.Phone, f.Company.CompanyName(), f.Company.Random.String(10), f.Random.Bool()));
	}
}

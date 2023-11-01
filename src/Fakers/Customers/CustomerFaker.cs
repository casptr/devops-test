using Domain.Customers;
using Fakers.Common;

namespace Fakers.Customers;

public class CustomerFaker : EntityFaker<Customer>
{
	private static int id = 100;
	public CustomerFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Customer(f.Person.FirstName, f.Person.LastName, new EmailAddress(f.Person.Email), f.Person.Phone, f.Company.CompanyName(), f.Company.Random.String(10))).RuleFor(f => f.Id, s => id++);
	}
}

using Domain.Supplements;
using Fakers.Common;

namespace Fakers.Supplements;

public class CategoryFaker : EntityFaker<Category>
{
	private static int id = 150;
	public CategoryFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Category(f.Commerce.ProductMaterial(), f.Random.Int(0, 10))).RuleFor(f => f.Id, s => id++);
	}
}
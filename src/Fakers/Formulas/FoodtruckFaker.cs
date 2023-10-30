using Domain.Formulas;
using Fakers.Common;

namespace Fakers.Formulas;

public class FoodtruckFaker : EntityFaker<Foodtruck>
{
	public FoodtruckFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Foodtruck());
	}
}

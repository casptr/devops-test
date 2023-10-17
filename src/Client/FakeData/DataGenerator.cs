using Bogus;

namespace Foodtruck.Client.FakeData;

public class DataGenerator
{
	Faker<FormulaFakeDataObj> formulaFake;

	public DataGenerator()
	{
		Randomizer.Seed = new Random();
		formulaFake = new Faker<FormulaFakeDataObj>()
			.RuleFor(u => u.Name, f => f.Company.Bs())
			.RuleFor(u => u.Description, f => f.Company.CatchPhrase())
			.RuleFor(u => u.Price, f => Math.Round(f.Random.Double(10.00, 500.00),2))
			.RuleFor(u => u.ImageUrl, f => f.Image.PicsumUrl());
	}	

	public IEnumerable<FormulaFakeDataObj> GenerateFormulas()
	{
		return formulaFake.GenerateForever();
	}
}

using Bogus;
using Domain;

namespace Foodtruck.Client.FakeData;

public class DataGenerator
{
	Faker<Formula> formulaFake;

	public DataGenerator()
	{
		Randomizer.Seed = new Random();
		formulaFake = new Faker<Formula>()
			.RuleFor(u => u.Name, f => f.Company.Bs())
			.RuleFor(u => u.Description, f => f.Company.CatchPhrase())
			.RuleFor(u => u.Price, f => Math.Round(f.Random.Double(10.00, 500.00),2))
			.RuleFor(u => u.ImageUrl, f => f.Image.PicsumUrl());
	}	

	public IEnumerable<Formula> GenerateFormulas()
	{
		return formulaFake.GenerateForever();
	}
}

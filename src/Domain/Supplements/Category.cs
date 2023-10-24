using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Supplements;

public class Category : Entity
{
	private string name = default!;
	public string Name
	{
		get => name;
		set => name = Guard.Against.NullOrWhiteSpace(value, nameof(Name));
	}
	private int vat = default!;
	public int Vat { get => vat; set => vat = Guard.Against.Negative(value, nameof(Vat)); }

	public Category(string name, int vat)
	{
		Vat = vat;
		Name = name;
	}
}

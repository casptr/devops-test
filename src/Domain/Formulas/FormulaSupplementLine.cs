using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Supplements;

namespace Domain.Formulas;

public class FormulaSupplementLine : Entity
{
	public Supplement Supplement { get; } = default!;
	public int Quantity { get; } = default!;

	/// <summary>
	/// Database Constructor
	/// </summary>
	private FormulaSupplementLine() { }

	public FormulaSupplementLine(SupplementItem item)
	{
		Supplement = Guard.Against.Null(item.Supplement, nameof(Supplement));
		Quantity = item.Quantity;
	}
}

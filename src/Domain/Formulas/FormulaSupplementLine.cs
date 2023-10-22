using Ardalis.GuardClauses;
using Domain.Common;
using Domain.QuotationsAndFormulas;

namespace Domain.Formulas
{
    public class FormulaSupplementLine : Entity
	{
		public Formula Formula { get; } = default!;
		public Supplement Supplement { get; } = default!;
		public int Quantity { get; } = default!;

		/// <summary>
		/// Database Constructor
		/// </summary>
		private FormulaSupplementLine() { }

		public FormulaSupplementLine(Formula formula, SupplementItem item)
		{
			Formula = Guard.Against.Null(formula, nameof(Formula));
			Supplement = Guard.Against.Null(item.Supplement, nameof(Supplement));
			Quantity = item.Quantity;
		}
	}
}

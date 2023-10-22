using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.QuotationsAndFormulas
{
	public class SupplementItem : ValueObject
	{
		public Supplement Supplement { get; }
		public int Quantity { get; }

		public SupplementItem(Supplement supplement, int quantity)
		{
			Supplement = Guard.Against.Null(supplement, nameof(Supplement));
			Quantity = quantity;
		}

		protected override IEnumerable<object?> GetEqualityComponents()
		{
			yield return Supplement;
		}
	}
}

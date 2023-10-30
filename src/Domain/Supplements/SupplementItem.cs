using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Supplements
{
	public class SupplementItem : ValueObject
	{
		public Supplement Supplement { get; } = default!;
		public int Quantity { get; } = default;
		
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

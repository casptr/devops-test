using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Supplements;

namespace Domain.Quotations;

public class Quotation : Entity
{
	public Customer Customer { get; } = default!;
	private readonly List<QuotationVersion> versions = new();
	public IReadOnlyCollection<QuotationVersion> Versions => versions.AsReadOnly();

	public void AddVersion(QuotationVersion version)
	{
		Guard.Against.Null(version, nameof(version));
		if (versions.Contains(version))
			throw new ApplicationException($"{nameof(Quotation)} '{Id}' already contains the version:{version.VersionNumber}");

		versions.Add(version);
	}

	private Quotation() { }

	public Quotation(Customer customer)
	{
		Customer = customer;
	}
}

public class QuotationVersion : Entity
{
	public int VersionNumber { get; } = default!;
	public int NumberOfGuests { get; } = default!;
	public string ExtraInfo { get; } = default!;
	public string Description { get; } = default!;
	public Money Price { get; } = default!;
	public Money VatTotal { get; } = default!;
	public Reservation Reservation { get; } = default!;
	public Formula Formula { get; } = default!;
	public Address EventAddress { get; } = default!;
	public Address BillingAddress { get; } = default!;

	//TODO transport cost

	private readonly List<QuotationSupplementLine> supplementLines = new();
	public IReadOnlyCollection<QuotationSupplementLine> SupplementLines => supplementLines.AsReadOnly();

	/// <summary>
	/// Database Constructor
	/// </summary>
	private QuotationVersion() { }

	public QuotationVersion(int numberOfGuests, string extraInfo, string description, Reservation reservation, Formula formula, IEnumerable<SupplementItem> supplementItems, Address eventAddress, Address billingAddress)
	{
		NumberOfGuests = Guard.Against.OutOfRange(numberOfGuests, nameof(NumberOfGuests), 0, 2000);
		ExtraInfo = Guard.Against.NullOrWhiteSpace(extraInfo, nameof(ExtraInfo));
		Description = Guard.Against.NullOrWhiteSpace(description, nameof(Description));
		Reservation = Guard.Against.Null(reservation, nameof(Reservation));
		Formula = Guard.Against.Null(formula, nameof(Formula));
		EventAddress = Guard.Against.Null(eventAddress, nameof(EventAddress));
		BillingAddress = Guard.Against.Null(billingAddress, nameof(BillingAddress));

		Price = new Money(supplementItems.Aggregate(0M, (total, next) => next.Supplement.Price.Value * new decimal(next.Quantity)));
		VatTotal = new Money(supplementItems.Aggregate(0M, (total, next) => next.Supplement.Price.Value * new decimal(next.Supplement.Category.Vat) / 100M));

		List<Supplement> supplementsIncludedInFormula = formula.IncludedSupplements
			.Select(s => s.Supplement)
			.Concat(formula.Choices
			.SelectMany(s => s.SupplementsToChoose)).ToList();

		foreach (SupplementItem item in supplementItems)
		{
			bool isIncludedInFormula = supplementsIncludedInFormula.Any(includedSupplement => includedSupplement.Equals(item.Supplement));
			supplementLines.Add(new QuotationSupplementLine(item, isIncludedInFormula));
		}
	}

}
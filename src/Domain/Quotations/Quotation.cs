using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Supplements;
using System.ComponentModel.DataAnnotations.Schema;

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
    private readonly List<QuotationSupplementLine> quotationSupplementLines = new();
    public IReadOnlyCollection<QuotationSupplementLine> QuotationSupplementLines => quotationSupplementLines.AsReadOnly();


    [NotMapped]
    public IReadOnlyCollection<QuotationSupplementLine> FormulaSupplementLines => quotationSupplementLines.Where(quotationSupplementLine => Formula.IncludedSupplements.Any(includedSupplement => includedSupplement.Supplement.Id == quotationSupplementLine.SupplementId) || Formula.Choices.SelectMany(choice => choice.SupplementsToChoose).Any(supplement => supplement.Id == quotationSupplementLine.SupplementId)).ToList().AsReadOnly();

    [NotMapped]
    public IReadOnlyCollection<QuotationSupplementLine> ExtraSupplementLines => quotationSupplementLines.Where(quotationSupplementLine => !FormulaSupplementLines.Contains(quotationSupplementLine)).ToList().AsReadOnly();


    /// <summary>
    /// Database Constructor
    /// </summary>
    private QuotationVersion() { }

    public QuotationVersion(int numberOfGuests, string extraInfo, string description, Reservation reservation, Formula formula, IEnumerable<SupplementItem> formulaSupplementItems, IEnumerable<SupplementItem> extraSupplementItems, Address eventAddress, Address billingAddress)
    {
        NumberOfGuests = Guard.Against.OutOfRange(numberOfGuests, nameof(NumberOfGuests), 0, 2000);
        ExtraInfo = Guard.Against.NullOrWhiteSpace(extraInfo, nameof(ExtraInfo));
        Description = Guard.Against.NullOrWhiteSpace(description, nameof(Description));
        Reservation = Guard.Against.Null(reservation, nameof(Reservation));
        Formula = Guard.Against.Null(formula, nameof(Formula));
        EventAddress = Guard.Against.Null(eventAddress, nameof(EventAddress));
        BillingAddress = Guard.Against.Null(billingAddress, nameof(BillingAddress));

        quotationSupplementLines.AddRange(formulaSupplementItems.Select(item => new QuotationSupplementLine(item)));
        quotationSupplementLines.AddRange(extraSupplementItems.Select(item => new QuotationSupplementLine(item)));

        var foodtruckPrice = formula.Foodtruck.CalculatePrice(((int)Math.Floor((reservation.End - reservation.Start).TotalDays))).Value;
        Price = new Money(quotationSupplementLines.Aggregate(0M, (total, next) => total + next.Price.Value * new decimal(next.Quantity)) + foodtruckPrice);
        VatTotal = new Money(quotationSupplementLines.Aggregate(0M, (total, next) => total + next.Vat.Value * new decimal(next.Quantity)) + foodtruckPrice * new decimal(Domain.Formulas.Foodtruck.VAT_PERCENTAGE) / 100M);


    }

}
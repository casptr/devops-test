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
    private int versionNumber;
    public int VersionNumber { get => versionNumber; set => versionNumber = Guard.Against.NegativeOrZero(value, nameof(VersionNumber)); }

    private int numberOfGuests;
    public int NumberOfGuests { get => numberOfGuests; set => numberOfGuests = Guard.Against.NegativeOrZero(value, nameof(NumberOfGuests)); }

    public string? ExtraInfo { get; set; }

    public string? Description { get; set; }

    private Reservation reservation;
    public Reservation Reservation { get => reservation; set => reservation = Guard.Against.Null(value, nameof(Reservation)); }

    private Formula formula;
    public Formula Formula { get => formula; set => formula = Guard.Against.Null(value, nameof(Formula)); }

    private Address eventAddress;
    public Address EventAddress { get => eventAddress; set => eventAddress = Guard.Against.Null(value, nameof(EventAddress)); }


    private Address billingAddress;
    public Address BillingAddress { get => billingAddress; set => billingAddress = Guard.Against.Null(value, nameof(BillingAddress)); }

    //TODO transport cost
    // TODO when foodtruck price changes quotation price will also change, need to fix

    private readonly List<QuotationSupplementLine> quotationSupplementLines = new();
    public IReadOnlyCollection<QuotationSupplementLine> QuotationSupplementLines => quotationSupplementLines.AsReadOnly();


    private Money foodtruckPrice;
    public Money FoodtruckPrice { get => foodtruckPrice; set => foodtruckPrice = Guard.Against.Null(value, nameof(FoodtruckPrice)); }

    private Money price;
    public Money Price { get => price; set => price = Guard.Against.Null(value, nameof(Price)); }

    private Money vatTotal;
    public Money VatTotal { get => vatTotal; set => vatTotal = Guard.Against.Null(value, nameof(VatTotal)); }

    /// <summary>
    /// Database Constructor
    /// </summary>
    private QuotationVersion() { }

    public QuotationVersion(int numberOfGuests, string extraInfo, string description, Reservation reservation, Formula formula, IEnumerable<SupplementItem> formulaSupplementItems, IEnumerable<SupplementItem> extraSupplementItems, Address eventAddress, Address billingAddress)
    {
        NumberOfGuests = numberOfGuests;
        ExtraInfo = extraInfo;
        Description = description;
        Reservation = reservation;
        Formula = formula;
        EventAddress = eventAddress;
        BillingAddress = billingAddress;
        VersionNumber = 1;

        SetQuotationSupplementLines(formulaSupplementItems, extraSupplementItems);
    }

    public void SetQuotationSupplementLines(IEnumerable<SupplementItem> formulaSupplementItems, IEnumerable<SupplementItem> extraSupplementItems)
    {
        Guard.Against.Null(formulaSupplementItems, nameof(formulaSupplementItems));
        Guard.Against.Null(extraSupplementItems, nameof(extraSupplementItems));

        quotationSupplementLines.Clear();
        quotationSupplementLines.AddRange(formulaSupplementItems.Select(item => new QuotationSupplementLine(item, true)));
        quotationSupplementLines.AddRange(extraSupplementItems.Select(item => new QuotationSupplementLine(item, false)));

        FoodtruckPrice = new Money(Formula.Foodtruck.CalculatePrice(((int)Math.Floor((Reservation.End - Reservation.Start).TotalDays))).Value);
        Price = new Money(QuotationSupplementLines.Aggregate(0M, (total, next) => total + next.SupplementPrice.Value * new decimal(next.Quantity)) + FoodtruckPrice.Value);
        VatTotal = new Money(QuotationSupplementLines.Aggregate(0M, (total, next) => total + next.SupplementVat.Value * new decimal(next.Quantity)) + FoodtruckPrice.Value * new decimal(Domain.Formulas.Foodtruck.VAT_PERCENTAGE) / 100M);
    }




}
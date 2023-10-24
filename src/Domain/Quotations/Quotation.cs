using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Supplements;
using static System.Net.Mime.MediaTypeNames;


namespace Domain.Quotations
{
    //public class Quotation : Entity
    //{
    //    private int numberOfGuests = default!;
    //    public int NumberOfGuests { get => numberOfGuests; set => numberOfGuests = Guard.Against.OutOfRange(value, nameof(NumberOfGuests), 0, 2000); } //TODO max number of guests

    //    private string extraInfo = default!;
    //    public string ExtraInfo { get => extraInfo; set => extraInfo = Guard.Against.NullOrWhiteSpace(value, nameof(ExtraInfo)); }

    //    private string description = default!;
    //    public string Description { get => description; set => description = Guard.Against.NullOrWhiteSpace(value, nameof(Description)); }

    //    private Money price = default!;
    //    public Money Price { get => price; set => price = Guard.Against.Null(value, nameof(price)); }

    //    private Money vatTotal = default!;
    //    public Money VatTotal { get => vatTotal; set => vatTotal = Guard.Against.Null(value, nameof(VatTotal)); }

    //    private Reservation reservation = default!;
    //    public Reservation Reservation { get => reservation; set => reservation = Guard.Against.Null(value, nameof(Reservation)); }

    //    private Formula formula = default!;
    //    public Formula Formula { get => formula; set => formula = Guard.Against.Null(value, nameof(Formula)); }

    //    private Address eventAddress = default!;
    //    public Address EventAddress { get => eventAddress; set => eventAddress = Guard.Against.Null(value, nameof(EventAddress)); }

    //    private Address billingAddress = default!;
    //    public Address BillingAddress { get => billingAddress; set => billingAddress = Guard.Against.Null(value, nameof(BillingAddress)); }

    //    private Customer customer = default!;
    //    public Customer Customer { get => customer; set => customer = Guard.Against.Null(value, nameof(Customer)); }

    //    private readonly List<QuotationSupplementLine> supplementLines = new();
    //    public IReadOnlyCollection<QuotationSupplementLine> SupplementLines => supplementLines.AsReadOnly();

    //    /// <summary>
    //    /// Database Constructor
    //    /// </summary>
    //    private Quotation() { }

    //    public Quotation(int numberOfGuests, string extraInfo, string description, Reservation reservation, Formula formula, IEnumerable<SupplementItem> supplementItem, Address eventAddress, Address billingAddress, Customer customer)
    //    {
    //        NumberOfGuests = numberOfGuests;
    //        ExtraInfo = extraInfo;
    //        Description = description;
    //        Reservation = reservation;
    //        Formula = formula;
    //        EventAddress = eventAddress;
    //        BillingAddress = billingAddress;
    //        Customer = customer;

    //        List<Supplement> supplementsIncludedInFormula = formula.IncludedSupplements
    //            .Select(s => s.Supplement)
    //            .Concat(formula.Choices
    //            .SelectMany(s => s.SupplementsToChoose)).ToList();

    //        foreach (SupplementItem item in supplementItem)
    //        {
    //            bool isIncludedInFormula = supplementsIncludedInFormula.Any(includedSupplement => includedSupplement.Equals(item.Supplement));
    //            supplementLines.Add(new QuotationSupplementLine(item, isIncludedInFormula));
    //        }
    //    }

    //}
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
}

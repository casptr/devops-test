using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Supplements;


namespace Domain.Quotations
{
    public class Quotation : Entity
    {
        private int numberOfGuests = default!;
        public int NumberOfGuests { get => numberOfGuests; set => numberOfGuests = Guard.Against.OutOfRange(value, nameof(NumberOfGuests), 0, 2000); }

        private string extraInfo = default!;
        public string ExtraInfo { get => extraInfo; set => extraInfo = Guard.Against.NullOrEmpty(value, nameof(ExtraInfo)); }

        private string description = default!;
        public string Description { get => description; set => description = Guard.Against.NullOrEmpty(value, nameof(Description)); }

        private Money price = default!;
        public Money Price { get => price; set => price = Guard.Against.Null(value, nameof(price)); }

        private Money vatTotal = default!;
        public Money VatTotal { get => vatTotal; set => vatTotal = Guard.Against.Null(value, nameof(VatTotal)); }

        private Reservation reservation = default!;
        public Reservation Reservation { get => reservation; set => reservation = Guard.Against.Null(value, nameof(Reservation)); }

        private Formula formula = default!;
        public Formula Formula { get => formula; set => formula = Guard.Against.Null(value, nameof(Formula)); }

        private Address eventAddress = default!;
        public Address EventAddress { get => eventAddress; set => eventAddress = Guard.Against.Null(value, nameof(EventAddress)); }

        private Customer customer = default!;
        public Customer Customer { get => customer; set => customer = Guard.Against.Null(value, nameof(Customer)); }

        private readonly List<QuotationSupplementLine> supplementLines = new();
        public IReadOnlyCollection<QuotationSupplementLine> SupplementLines => supplementLines.AsReadOnly();

        /// <summary>
        /// Database Constructor
        /// </summary>
        private Quotation() { }

        public Quotation(int numberOfGuests, string extraInfo, string description, Reservation reservation, Formula formula, IEnumerable<SupplementItem> supplementItem, Address eventAddress, Customer customer)
        {
            NumberOfGuests = numberOfGuests;
            ExtraInfo = extraInfo;
            Description = description;
            Reservation = reservation;
            Formula = formula;
            EventAddress = eventAddress;
            Customer = customer;

            List<Supplement> supplementsIncludedInFormula = formula.IncludedSupplements
                .Select(s => s.Supplement)
                .Concat(formula.Choices
                .SelectMany(s => s.SupplementsToChoose)).ToList();

            foreach (SupplementItem item in supplementItem)
            {
                bool isIncludedInFormula = supplementsIncludedInFormula.Any(includedSupplement => includedSupplement.Equals(item.Supplement));
                supplementLines.Add(new QuotationSupplementLine(item, isIncludedInFormula));
            }
        }

    }
}

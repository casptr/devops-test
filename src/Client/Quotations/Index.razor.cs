using Domain.Quotations;
using Domain.Formulas;
using Ardalis.GuardClauses;
using Domain.Supplements;
using Domain.Customers;
using Address = Domain.Customers.Address;
using Domain.Common;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Foodtruck.Client.QuotationProcess.Helpers;
using Microsoft.AspNetCore.Components;

namespace Foodtruck.Client.Quotations
{
    public partial class Index
    {
        private string title = "Title";
        private string description = "description";
        private Money price = new Money(203.5M);
        private Uri imageUrl = new Uri("https://www.example.com");
        private DateTime nextWeek = DateTime.Now.AddDays(7);
        private DateTime nextWeekPlus2 = DateTime.Now.AddDays(9);
        public FrontendCustomer customer;
        public FrontendQuotationVersion quotationVersion;

        [Inject] public QuotationProcessState QuotationProcessState { get; set; }

        public class CustomerDetailsModel
        {
            public string? Firstname { get; set; }
            public string? Lastname { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? CompanyName { get; set; }
            public string? CompanyNumber { get; set; }
            public string? BillingAddress_zip { get; set; }
            public string? BillingAddress_city { get; set; }
            public string? BillingAddress_street { get; set; }
            public string? BillingAddress_houseNumber { get; set; }
            public string? EventAddress_zip { get; set; }
            public string? EventAddress_city { get; set; }
            public string? EventAddress_street { get; set; }
            public string? EventAddress_houseNumber { get; set; }
            public CustomerDetailsModel()
            {
                Firstname = "John";
                Lastname = "Doe";
                Email = "john.doe@example.com";
                Phone = "123-456-7890";
                CompanyName = "Example Company";
                CompanyNumber = "1234567";
                BillingAddress_street = "123 Billing St";
                BillingAddress_houseNumber = "42";
                BillingAddress_zip = "12345";
                BillingAddress_city = "Billing City";
                EventAddress_street = "456 Event St";
                EventAddress_houseNumber = "77";
                EventAddress_zip = "54321";
                EventAddress_city = "Event City";
            }
        }

        public class FormulaChoicesModel
        {
            public List<FormulaSupplementChoiceDto.Detail>? IncludedSupplements { get; set; }
            public List<SupplementDto.Detail>? ChosenSupplements { get; set; }
            public int NumberOfGuests { get; set; }
            public FormulaChoicesModel()
            {
                NumberOfGuests = 1;
            }
        }

        public CustomerDetailsModel customerDetailsModel = new CustomerDetailsModel();
        public FormulaChoicesModel formulaChoicesModel = new FormulaChoicesModel();

        private void SubmitForm()
        {
            QuotationProcessState.RequestQuotation();
            QuotationProcessState.PrintQuotation();

            if (formulaChoicesModel != null && formulaChoicesModel.ChosenSupplements != null)
            {
                customer = new FrontendCustomer(
                    customerDetailsModel.Firstname,
                    customerDetailsModel.Lastname,
                    new EmailAddress(customerDetailsModel.Email),
                    customerDetailsModel.Phone,
                    customerDetailsModel.CompanyName,
                    customerDetailsModel.CompanyNumber,
                    false
                );

                List<SupplementItem> supplementChoices = new List<SupplementItem>();
                foreach (var supplement in formulaChoicesModel.ChosenSupplements)
                {
                    supplementChoices.Add(new SupplementItem(new Supplement(supplement.Name, supplement.Description, new Category(supplement.Category.Name, 21), new Money(supplement.Price), supplement.AmountAvailable), 1));
                }

                Console.WriteLine("Supplement Choices:");
                foreach (var chosenSupplement in supplementChoices)
                {
                    Console.WriteLine($"Name: {chosenSupplement.Supplement.Name}");
                    Console.WriteLine($"Description: {chosenSupplement.Supplement.Description}");
                    Console.WriteLine($"Category: {chosenSupplement.Supplement.Category.Name}");
                    Console.WriteLine($"Price: {chosenSupplement.Supplement.Price}");
                    Console.WriteLine($"AmountAvailable: {chosenSupplement.Supplement.AmountAvailable}");
                    Console.WriteLine($"Quantity: {chosenSupplement.Quantity}");
                    Console.WriteLine();
                }

                quotationVersion = new FrontendQuotationVersion(
                    formulaChoicesModel.NumberOfGuests, "extrainfo", "description",
                    new Reservation(nextWeek, nextWeekPlus2, "reservationdescription", Status.VOORGESTELD),
                    new Formula(title, description, price, imageUrl),
                    supplementChoices,
                    new Address(customerDetailsModel.BillingAddress_zip, customerDetailsModel.BillingAddress_city, customerDetailsModel.BillingAddress_street, customerDetailsModel.BillingAddress_houseNumber),
                    new Address(customerDetailsModel.EventAddress_zip, customerDetailsModel.EventAddress_city, customerDetailsModel.EventAddress_street, customerDetailsModel.EventAddress_houseNumber)
                );


                Console.WriteLine("Customer Details:");
                Console.WriteLine($"Firstname: {customer.Firstname}");
                Console.WriteLine($"Lastname: {customer.Lastname}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phone: {customer.Phone}");
                Console.WriteLine($"CompanyName: {customer.CompanyName}");
                Console.WriteLine($"CompanyNumber: {customer.CompanyNumber}");
                Console.WriteLine($"WantsMarketingMails: {customer.WantsMarketingMails}");

                Console.WriteLine("\nQuotation Version Details:");
                Console.WriteLine($"NumberOfGuests: {quotationVersion.NumberOfGuests}");
                Console.WriteLine($"ExtraInfo: {quotationVersion.ExtraInfo}");
                Console.WriteLine($"Description: {quotationVersion.Description}");
                Console.WriteLine($"Price: {quotationVersion.Price}");
                Console.WriteLine($"VatTotal: {quotationVersion.VatTotal}");
                Console.WriteLine($"Reservation: {quotationVersion.Reservation}");
                Console.WriteLine($"Formula: {quotationVersion.Formula}");
                Console.WriteLine($"EventAddress: {quotationVersion.EventAddress.Street}");
                Console.WriteLine($"BillingAddress: {quotationVersion.BillingAddress}");
                Console.WriteLine("Supplement Lines:");
                foreach (var supplementLine in quotationVersion.supplementLines)
                {
                    Console.WriteLine($"Item: {supplementLine.Name}");
                    Console.WriteLine($"IsIncludedInFormula: {supplementLine.IncludedInFormula}");
                }
            }
            else
            {
                Console.WriteLine("something wrong with formulachoicesmodel");
            }
        }


    }
    public class FrontendQuotationVersion
    {
        public int NumberOfGuests { get; set; } = default!;
        public string ExtraInfo { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal VatTotal { get; set; } = default!;
        public Reservation Reservation { get; set; } = default!;
        public Formula Formula { get; set; } = default!;
        public Address EventAddress { get; set; } = default!;
        public Address BillingAddress { get; set; } = default!;
        public List<QuotationSupplementLine> supplementLines = new List<QuotationSupplementLine>();

        public FrontendQuotationVersion() { }
        public FrontendQuotationVersion(int numberOfGuests, string extraInfo, string description, Reservation reservation, Formula formula, IEnumerable<SupplementItem> supplementItems, Address eventAddress, Address billingAddress)
        {
            NumberOfGuests = Guard.Against.OutOfRange(numberOfGuests, nameof(NumberOfGuests), 0, 2000);
            ExtraInfo = Guard.Against.NullOrWhiteSpace(extraInfo, nameof(ExtraInfo));
            Description = Guard.Against.NullOrWhiteSpace(description, nameof(Description));
            Reservation = Guard.Against.Null(reservation, nameof(Reservation));
            Formula = Guard.Against.Null(formula, nameof(Formula));
            EventAddress = Guard.Against.Null(eventAddress, nameof(EventAddress));
            BillingAddress = Guard.Against.Null(billingAddress, nameof(BillingAddress));

            Price = new decimal((double)supplementItems.Aggregate(0M, (total, next) => next.Supplement.Price.Value * new decimal(next.Quantity)));
            VatTotal = new decimal((double)supplementItems.Aggregate(0M, (total, next) => next.Supplement.Price.Value * new decimal(next.Supplement.Category.Vat) / 100M));

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

public class FrontendCustomer
{
    private string firstname = default!;
    public string Firstname { get => firstname; set => firstname = Guard.Against.NullOrWhiteSpace(value, nameof(Firstname)); }

    private string lastname = default!;
    public string Lastname
    {
        get => lastname;
        set => lastname = Guard.Against.NullOrWhiteSpace(value, nameof(Lastname));
    }

    private EmailAddress email = default!;
    public EmailAddress Email
    {
        get => email;
        set => email = Guard.Against.Null(value, nameof(Email));
    }

    private string phone = default!;
    public string Phone { get => phone; set => phone = Guard.Against.Null(value, nameof(Phone)); }

    public string? CompanyName { get; set; }
    public string? CompanyNumber { get; set; }
    public bool WantsMarketingMails { get; set; } = default!;

    public FrontendCustomer() { }

    public FrontendCustomer(string firstname, string lastname, EmailAddress email, string phone, string? companyName, string? companyNumber, bool wantsMarketingMails)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Phone = phone;
        CompanyName = companyName;
        CompanyNumber = companyNumber;
        WantsMarketingMails = wantsMarketingMails;
    }
}


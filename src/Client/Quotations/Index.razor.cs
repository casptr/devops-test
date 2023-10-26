using Microsoft.AspNetCore.Components;
using Foodtruck.Shared.Quotations;
using MudBlazor;
using static Foodtruck.Shared.Quotations.QuotationDto;
using Domain.Quotations;
using Bogus.DataSets;
using Domain.Formulas;
using Ardalis.GuardClauses;
using Domain.Supplements;
using Domain.Customers;
using Address = Domain.Customers.Address;
using Domain.Common;

namespace Foodtruck.Client.Quotations;

public partial class Index
{
    public FrontendCustomer? customer;
    public FrontendQuotationVersion? quotationVersion;
    private string? firstname;
    private string lastname;
    private string email;
    private string phone;
    private string? companyName;
    private string? companyNumber;
    private string billingAddress_zip;
    private string billingAddress_city;
    private string billingAddress_street;
    private string billingAddress_houseNumber;
    private string eventAddress_zip;
    private string eventAddress_city;
    private string eventAddress_street;
    private string eventAddress_houseNumber;
    private string title ="Title";
    private string description = "description";
    private Money price = new Money(203.5M);
    private Uri imageUrl = new Uri("https://www.example.com/");

    private DateTime nextWeek = DateTime.Now.AddDays(7);
    private DateTime nextWeekPlus2 = DateTime.Now.AddDays(9);

    private void SubmitForm()
    {
        
        customer = new FrontendCustomer(
            firstname,
            lastname, 
            new EmailAddress(email),
            phone,
            companyName,
            companyNumber,
            false
        );

        quotationVersion = new FrontendQuotationVersion(
            1, "extrainfo","description",
            new Reservation(nextWeek, nextWeekPlus2, "reservationdescription", Status.VOORGESTELD),
            new Formula(title, description, price, imageUrl),
            new List<SupplementItem>(),
            new Address(billingAddress_zip, billingAddress_city, billingAddress_street, billingAddress_houseNumber),
            new Address(eventAddress_zip, eventAddress_city, eventAddress_street, eventAddress_houseNumber)

        );
    }
}

    public class FrontendQuotationVersion {
        public int NumberOfGuests { get; set;} = default!;
        public string ExtraInfo { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal VatTotal { get; set; } = default!;
        public Reservation Reservation { get; set; } = default!;
        public Formula Formula { get; set; } = default!;
        public Address EventAddress { get; set; } = default!;
        public Address BillingAddress { get; set; } = default!;
        public List<QuotationSupplementLine> supplementLines = new();

        public FrontendQuotationVersion(){}
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



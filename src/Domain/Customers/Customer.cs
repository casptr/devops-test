using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Customers;
public class Customer : Entity // TODO make customer valueobject?
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


    public int QuotationId { get; set; }

    /// <summary>
    /// Database Constructor
    /// </summary>
    private Customer() { }

	public Customer(string firstname, string lastname, EmailAddress email, string phone, string? companyName = null, string? companyNumber = null)
	{
		Firstname = firstname;
		Lastname = lastname;
		Email = email;
		Phone = phone;
		CompanyName = companyName;
		CompanyNumber = companyNumber;
	}
}




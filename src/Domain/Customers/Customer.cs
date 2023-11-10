using Ardalis.GuardClauses;
using Domain.Common;
using System.IO;

namespace Domain.Customers;

public class Customer : ValueObject
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

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Email;
        yield return Firstname.ToLower();
        yield return Lastname.ToLower();
        yield return Phone.ToLower();
        yield return CompanyNumber?.ToLower();
        yield return CompanyName?.ToLower();
    }
}




using Ardalis.GuardClauses;
using Domain.Common;
using Quotation;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	internal class AllClasses
	{
	}

	public class Address : ValueObject
	{
		public string Zip { get; }
		public string City { get; }
		public string Street { get; }
		public string HouseNumber { get; }

		public Address(string zip, string city, string street, string houseNumber)
		{
			Zip = Guard.Against.NullOrWhiteSpace(zip, nameof(Zip));
			City = Guard.Against.NullOrWhiteSpace(city, nameof(City));
			Street = Guard.Against.NullOrWhiteSpace(street, nameof(Street));
			HouseNumber = Guard.Against.NullOrWhiteSpace(houseNumber, nameof(HouseNumber));
		}

		protected override IEnumerable<object?> GetEqualityComponents()
		{
			yield return Zip.ToLower();
			yield return City.ToLower();
			yield return Street.ToLower();
			yield return HouseNumber.ToLower();
		}
	}

	public class EmailAddress : ValueObject
	{
		public string Value { get; } = default!;

		/// <summary>
		/// Database Constructor
		/// </summary>
		private EmailAddress() { }

		public EmailAddress(string value)
		{
			if (!IsValid(value))
				throw new ApplicationException($"Invalid {nameof(EmailAddress)}: {value}");

			Value = value.Trim();
		}

		private static bool IsValid(string emailaddress)
		{
			try
			{
				MailAddress m = new(emailaddress);

				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value.ToLowerInvariant();
		}
	}

	public class Customer : Entity
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
		public string Phone { get => phone; set => Guard.Against.Null(value, nameof(Phone)); }


		public string? CompanyName { get; set; }
		public string? CompanyNumber { get; set; }



		public bool WantsMarketingMails { get; set; }


		private Address address = default!;
		public Address Address
		{
			get => address;
			set => address = Guard.Against.Null(value, nameof(Address));
		}

		/// <summary>
		/// Database Constructor
		/// </summary>
		public Customer()
		{

		}

		public Customer(string firstname, string lastname, EmailAddress email, string phone, Address address, string? companyName, string? companyNumber)
		{
			Firstname = firstname;
			Lastname = lastname;
			Email = email;
			Phone = phone;
			Address = address;
			CompanyName = companyName;
			CompanyNumber = companyNumber;
		}
	}


	public class FormulaSupplementChoice : Entity
	{
		public string Name { get; set; }
		public bool IsQuantityNumberOfGuests { get; set; }
		public int MinQuantity { get; set; }

		public Supplement DefaultChoice { get; set; }

		private readonly List<Supplement> supplementsToChoose = new();
		public IReadOnlyCollection<Supplement> SupplementsToChoose => supplementsToChoose.AsReadOnly();
	}

	public class Formula : Entity
	{
		private string title = default!;
		public string Title { get => title; set => title = Guard.Against.NullOrWhiteSpace(value, nameof(Title)); }

		private string description = default!;
		public string Description { get => description; set => description = Guard.Against.NullOrWhiteSpace(value, nameof(Description)); }

		private Money price = default!;
		public Money Price { get => price; set => price = Guard.Against.Null(value, nameof(Price)); }

		private string imageUrl = default!;
		public string ImageUrl { get => imageUrl; set => imageUrl = Guard.Against.NullOrWhiteSpace(value, nameof(ImageUrl)); }

		private readonly List<FormulaSupplementChoice> choices = new();
		public IReadOnlyCollection<FormulaSupplementChoice> Choices => choices.AsReadOnly();

		private readonly List<SupplementLine> includedSupplements = new();
		public IReadOnlyCollection<SupplementLine> IncludedSupplements => includedSupplements.AsReadOnly();


		/// <summary>
		/// Database Constructor
		/// </summary>
		public Formula()
		{

		}

		public Formula(string title, string description, Money price, string imageUrl)
		{
			Title = title;
			Description = description;
			Price = price;
			ImageUrl = imageUrl;
		}

		public void AddIncludedSupplementLine(SupplementLine supplementLine)
		{
			Guard.Against.Null(supplementLine, nameof(supplementLine));
			if (includedSupplements.Contains(supplementLine))
				throw new ApplicationException($"{nameof(Formula)} '{title}' already contains the supplement:{supplementLine.Supplement.Name}");

			includedSupplements.Add(supplementLine);
		}
	}

	public class SupplementLine : Entity
	{
		public Supplement Supplement { get; } = default!;

		private int quantity = default!;
		public int Quantity { get => quantity; set => Guard.Against.OutOfRange(value, nameof(Quantity), 0, Supplement.AmountAvailable); }

		/// <summary>
		/// Database Constructor
		/// </summary>
		public SupplementLine()
		{

		}

		public SupplementLine(Supplement supplement, int quantity)
		{
			Supplement = Guard.Against.Null(supplement, nameof(Supplement));
			Quantity = quantity;
		}
	}

	public class PackageSupplementLine : Entity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public Money Price { get; set; }
		public int Quantity { get; set; }
	}

	public class Quotation : Entity
	{
		private int numberOfGuests = default!;
		public int NumberOfGuests { get => numberOfGuests; set => numberOfGuests = Guard.Against.OutOfRange(value, nameof(NumberOfGuests), 0, 2000); }

		public string ExtraInfo { get; set; }
		public string Description { get; set; }
		public Money Price { get; set; }
		public Money VatTotal { get; set; }
		public Reservation Reservation { get; set; }
		public Package Package { get; set; }
		public Address EventAddress { get; set; }
		public Customer Customer { get; set; }



	}

	public class Package : Entity
	{
		public Formula Formula { get; set; }
		public Money Price { get; set; }
		public Money VatTotal { get; set; }

		private readonly List<PackageSupplementLine> allSupplements = new();
		public IReadOnlyCollection<PackageSupplementLine> AllSupplements => allSupplements.AsReadOnly();
	}


	public class Reservation : Entity
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string Description { get; set; }
		public Status Status { get; set; }
	}

	//TODO define the possible statusses
	public enum Status
	{
		VOORGESTELD,
		BEVESTIGD,
		BETAALD
	}


}

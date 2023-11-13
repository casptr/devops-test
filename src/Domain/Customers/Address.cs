using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Customers;

public class Address : ValueObject
{
    public string Zip { get; } = default!;
    public string City { get; } = default!;
    public string Street { get; } = default!;
    public string HouseNumber { get; } = default!;

    public Address(string zip, string city, string street, string houseNumber)
    {
        Zip = Guard.Against.NullOrWhiteSpace(zip, nameof(Zip));
        City = Guard.Against.NullOrWhiteSpace(city, nameof(City));
        Street = Guard.Against.NullOrWhiteSpace(street, nameof(Street));
        HouseNumber = Guard.Against.NullOrWhiteSpace(houseNumber, nameof(HouseNumber));
    }

    private Address() { }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Zip.ToLower();
        yield return City.ToLower();
        yield return Street.ToLower();
        yield return HouseNumber.ToLower();
    }
}





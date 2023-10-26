using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;

namespace Foodtruck.Shared.Quotations;

public abstract class QuotationRequest
{
    public class QuotationVersion{
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
    }
}
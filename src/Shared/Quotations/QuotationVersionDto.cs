using Foodtruck.Shared.Customers;
using Foodtruck.Shared.Reservations;
using Foodtruck.Shared.Supplements;

namespace Foodtruck.Shared.Quotations
{
    public abstract class QuotationVersionDto
    {
        public class Create
        {
            public int NumberOfGuests { get; set; }
            public string? ExtraInfo { get; set; }
            public int? FormulaId { get; set; }
            public ReservationDto.Create Reservation { get; set; } = new();
            public AddressDto EventAddress { get; set; } = new();
            public AddressDto BillingAddress { get; set; } = new();
            public List<SupplementItemDto.Create> Items { get; set; } =  new();
        }
    }
}

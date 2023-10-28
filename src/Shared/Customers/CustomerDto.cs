using Ardalis.GuardClauses;
using Domain.Customers;

namespace Foodtruck.Shared.Customers
{
    public abstract class CustomerDto
    {
        public class Create
        {
            public string? Firstname { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? CompanyName { get; set; }
            public string? CompanyNumber { get; set; }
            public bool WantsMarketingMails { get; set; }
        }
    }
}
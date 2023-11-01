using Ardalis.GuardClauses;
using Domain.Customers;
using FluentValidation;

namespace Foodtruck.Shared.Customers
{
    public abstract class CustomerDto
    {
        public class Create
        {
            public string? Firstname { get; set; }
            public string? Lastname { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? CompanyName { get; set; }
            public string? CompanyNumber { get; set; }
            public bool WantsMarketingMails { get; set; } = true;


            public class Validator : FluentValidator<Create>
            {
                public Validator()
                {
                    RuleFor(c => c.Firstname).NotEmpty().MaximumLength(20).MinimumLength(3);
                }
            }
        }
    }
}
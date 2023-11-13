using Ardalis.GuardClauses;
using Domain.Customers;
using FluentValidation;

namespace Foodtruck.Shared.Customers
{
    public abstract class CustomerDto
    {
        public class Detail
        {
            public string? Firstname { get; set; }
            public string? Lastname { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? CompanyName { get; set; }
            public string? CompanyNumber { get; set; }
        }

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
                    RuleFor(c => c.Firstname).NotEmpty().MaximumLength(40).MinimumLength(2);
                    RuleFor(c => c.Lastname).NotEmpty().MaximumLength(40).MinimumLength(2);
                    RuleFor(c => c.Email).NotEmpty().EmailAddress();
                    RuleFor(c => c.Phone).NotEmpty().MaximumLength(15);
                    When(x => !string.IsNullOrEmpty(x.CompanyName) || !string.IsNullOrEmpty(x.CompanyNumber), () =>
                    {
                        RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(50);
                        RuleFor(x => x.CompanyNumber).NotEmpty().MaximumLength(15);
                    });
                }
            }
        }
    }
}
using Foodtruck.Shared.Customers;
using Foodtruck.Shared;
using FluentValidation;

namespace Foodtruck.Client.QuotationProcess.Helpers
{
    public class CustomerDetailsFormModel
    {
        public CustomerDto.Create Customer { get; set; } = new();
        public AddressDto EventAddress { get; set; } = new();
        public AddressDto BillingAddress { get; set; } = new();
        public bool IsEventAddressDifferentThanBillingAddress { get; set; }
        public string? ExtraInfo { get; set; }

        public class Validator : FluentValidator<CustomerDetailsFormModel>
        {
            public Validator()
            {
                RuleFor(x => x.Customer).NotNull().SetValidator(new CustomerDto.Create.Validator());
                RuleFor(x => x.EventAddress).NotNull().SetValidator(new AddressDto.Validator());
                When(x => x.IsEventAddressDifferentThanBillingAddress, () =>
                {
                    RuleFor(x => x.BillingAddress).NotNull().SetValidator(new AddressDto.Validator());
                });
                RuleFor(x => x.ExtraInfo).MaximumLength(1000);

            }
        }
    }
}

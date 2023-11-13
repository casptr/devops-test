using FluentValidation;

namespace Foodtruck.Shared.Customers
{
    public class AddressDto
    {
        public string? Zip { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }

        public class Validator : FluentValidator<AddressDto>
        {
            public Validator()
            {
                RuleFor(x => x.Zip).NotEmpty().MaximumLength(20);
                RuleFor(x => x.Street).NotEmpty().MaximumLength(250);
                RuleFor(x => x.City).NotEmpty().MaximumLength(100);
                RuleFor(x => x.HouseNumber).NotEmpty().MaximumLength(10);
            }
        }
    }
}

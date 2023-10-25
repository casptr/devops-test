using Domain.Supplements;
using FluentValidation;

namespace Foodtruck.Shared.Supplements;

public abstract class SupplementDto
{
    public class Index
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int AmountAvailable { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Uri>? ImageUrls { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // Worden alle values meegestuurd of enkel degene die geweizigd zijn
    public class Mutate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int AmountAvailable { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Uri>? ImageUrls { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(s => s.Name).NotEmpty().MaximumLength(20).MinimumLength(3);
                RuleFor(s => s.Description).NotEmpty().MaximumLength(100).MinimumLength(3);
                RuleFor(s => s.Category).NotNull();
                RuleFor(s => s.AmountAvailable).InclusiveBetween(0, int.MaxValue);
                RuleFor(s => s.Price).InclusiveBetween(0, Decimal.MaxValue);
            }
        }
    }
}

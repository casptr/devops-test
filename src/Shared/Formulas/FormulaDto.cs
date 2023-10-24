using FluentValidation;

namespace Foodtruck.Shared.Formulas;

public abstract class FormulaDto
{
    public class Index
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class Detail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Uri? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<string>? IncludedSupplements { get; set; }
        public IEnumerable<string>? Choices { get; set; }
    }

    // Worden alle values meegestuurd of enkel degene die geweizigd zijn
    public class Mutate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Uri? ImageUrl { get; set; }
        public IEnumerable<string>? IncludedSupplements { get; set; }
        public IEnumerable<string>? Choices { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(s => s.Title).NotEmpty().MaximumLength(20).MinimumLength(3);
                RuleFor(s => s.Description).NotEmpty().MaximumLength(100).MinimumLength(3);
                RuleFor(s => s.Price).InclusiveBetween(0, Decimal.MaxValue);
                RuleFor(s => s.IncludedSupplements).NotEmpty();
                RuleFor(s => s.Choices).NotEmpty();
            }
        }
    }
}

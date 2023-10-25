using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Formulas
{
    public class Formula : Entity
    {
        public Foodtruck Foodtruck { get; } = default!;

        private string title = default!;
        public string Title { get => title; set => title = Guard.Against.NullOrWhiteSpace(value, nameof(Title)); }

        private string description = default!;
        public string Description { get => description; set => description = Guard.Against.NullOrWhiteSpace(value, nameof(Description)); }

        private Money price = default!;
        public Money Price { get => price; set => price = Guard.Against.Null(value, nameof(Price)); }

        private Uri imageUrl = default!;
        public Uri ImageUrl { get => imageUrl; set => imageUrl = Guard.Against.Null(value, nameof(ImageUrl)); }

        private readonly List<FormulaSupplementChoice> choices = new();
        public IReadOnlyCollection<FormulaSupplementChoice> Choices => choices.AsReadOnly();

        private readonly List<FormulaSupplementLine> includedSupplements = new();
        public IReadOnlyCollection<FormulaSupplementLine> IncludedSupplements => includedSupplements.AsReadOnly();

        /// <summary>
        /// Database Constructor
        /// </summary>
        private Formula() { }

        public Formula(Foodtruck foodtruck, string title, string description, Money price, Uri imageUrl)
        {
            Foodtruck = Guard.Against.Null(foodtruck, nameof(Foodtruck));
            Title = title;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }

        public void AddIncludedSupplementLine(FormulaSupplementLine supplementLine)
        {
            Guard.Against.Null(supplementLine, nameof(supplementLine));
            if (includedSupplements.Contains(supplementLine))
                throw new ApplicationException($"{nameof(Formula)} '{title}' already contains the supplement:{supplementLine.Supplement.Name}");

            includedSupplements.Add(supplementLine);
        }

        public void AddSupplemenChoice(FormulaSupplementChoice supplementChoice)
        {
            Guard.Against.Null(supplementChoice, nameof(supplementChoice));
            if (choices.Contains(supplementChoice))
                throw new ApplicationException($"{nameof(Formula)} '{title}' already contains the supplementChoice:{supplementChoice.Name}");

            choices.Add(supplementChoice);
        }
    }
}

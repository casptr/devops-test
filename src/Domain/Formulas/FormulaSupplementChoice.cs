using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Supplements;

namespace Domain.Formulas
{
    public class FormulaSupplementChoice : Entity
    {
        private string name = default!;
        public string Name { get => name; set => name = Guard.Against.NullOrWhiteSpace(value, nameof(Name)); }

        private int minQuantity = default!;
        public int MinQuantity { get => minQuantity; set => minQuantity = Guard.Against.Negative(value, nameof(MinQuantity)); }

        private Supplement defaultChoice = default!;
        public Supplement DefaultChoice { get => defaultChoice; set => defaultChoice = Guard.Against.Null(value, nameof(DefaultChoice)); }

        private readonly List<Supplement> supplementsToChoose = new();
        public IReadOnlyCollection<Supplement> SupplementsToChoose => supplementsToChoose.AsReadOnly();

        /// <summary>
        /// Database Constructor
        /// </summary>
        private FormulaSupplementChoice() { }

        public FormulaSupplementChoice(string name, int minQuantity, Supplement defaultChoice)
        {
            Name = name;
            MinQuantity = minQuantity;
            DefaultChoice = defaultChoice;
        }

        public void AddSupplementToChoose(Supplement supplement)
        {
            Guard.Against.Null(supplement, nameof(supplement));
            supplementsToChoose.Add(supplement);
        }

    }
}

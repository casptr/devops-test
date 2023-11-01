using Domain.Formulas;
using Foodtruck.Shared.Formulas;

namespace Foodtruck.Client.QuotationProcess.Helpers
{
    public class FormulaChoice
    {
        public int MinQuantity { get; }
        private readonly List<FormulaChoiceItem> options = new();
        public IReadOnlyList<FormulaChoiceItem> Options => options.AsReadOnly();

        public FormulaChoice(FormulaSupplementChoiceDto.Detail choice)
        {
            MinQuantity = choice.MinQuantity;
            options.AddRange(choice.SupplementsToChoose.Select(supplementToChoose =>
            {
                int initialQuantity = choice.DefaultChoice == supplementToChoose ? choice.MinQuantity : 0;
                return new FormulaChoiceItem(supplementToChoose, initialQuantity);
            }));
        }
    }
}

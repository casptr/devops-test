using Domain.Formulas;
using Foodtruck.Shared.Formulas;

namespace Foodtruck.Client.QuotationProcess.Helpers
{
    public class FormulaChoice
    {
        public bool IsQuantityNumberOfGuests { get; }
        public int MinQuantity { get; }
        private readonly List<FormulaChoiceItem> options = new();
        public IReadOnlyList<FormulaChoiceItem> Options => options.AsReadOnly();

        public FormulaChoice(FormulaSupplementChoiceDto.Detail choice)
        {
            IsQuantityNumberOfGuests = choice.IsQuantityNumberOfGuests;
            MinQuantity = choice.MinQuantity;

            options.AddRange(choice.SupplementsToChoose.Select(supplementToChoose => new FormulaChoiceItem(supplementToChoose, choice.DefaultChoice == supplementToChoose && !choice.IsQuantityNumberOfGuests ? choice.MinQuantity : 0, choice.DefaultChoice == supplementToChoose && !choice.IsQuantityNumberOfGuests)));
        }
    }
}

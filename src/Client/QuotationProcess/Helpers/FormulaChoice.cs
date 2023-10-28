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
            options.AddRange(choice.SupplementsToChoose.Select(supplementToChoose =>
            {
                int initialQuantity = choice.DefaultChoice == supplementToChoose && !choice.IsQuantityNumberOfGuests ? choice.MinQuantity : 0;
                bool isChosen = choice.DefaultChoice == supplementToChoose && choice.IsQuantityNumberOfGuests;
                return new FormulaChoiceItem(supplementToChoose, initialQuantity, isChosen);
            }));
        }
    }
}

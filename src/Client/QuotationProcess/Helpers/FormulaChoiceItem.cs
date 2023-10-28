using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;

namespace Foodtruck.Client.QuotationProcess.Helpers
{
    public class FormulaChoiceItem
    {
        public SupplementDto.Detail Supplement { get; set; }
        public int Quantity { get; set; }
        public bool IsChosen { get; set; }

        public FormulaChoiceItem(SupplementDto.Detail supplement, int defaultQuantity, bool isChosen = true)
        {
            Supplement = supplement;
            Quantity = defaultQuantity;
            IsChosen = isChosen;
        }
    }
}

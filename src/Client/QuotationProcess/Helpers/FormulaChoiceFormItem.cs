using Foodtruck.Shared.Customers;
using Foodtruck.Shared;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using FluentValidation;

namespace Foodtruck.Client.QuotationProcess.Helpers
{
    public class FormulaChoiceFormItem
    {
        public SupplementDto.Detail Supplement { get; set; }
        public int Quantity { get; set; }

        public FormulaChoiceFormItem(SupplementDto.Detail supplement, int defaultQuantity)
        {
            Supplement = supplement;
            Quantity = defaultQuantity;
        }


        public class Validator : FluentValidator<FormulaChoiceFormItem>
        {
            public Validator()
            {
                RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            }
        }
    }
}

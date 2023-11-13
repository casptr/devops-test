using Domain.Formulas;
using Foodtruck.Shared.Customers;
using Foodtruck.Shared;
using Foodtruck.Shared.Formulas;
using FluentValidation;

namespace Foodtruck.Client.QuotationProcess.Helpers
{
    public class FormulaChoiceFormModel
    {
        public int MinQuantity { get; }
        private readonly List<FormulaChoiceFormItem> options = new();
        public IReadOnlyList<FormulaChoiceFormItem> Options => options.AsReadOnly();

        public bool IsMinimumQuantityReached => options.Aggregate(0, (totalQuantity, item) => totalQuantity + item.Quantity) >= MinQuantity;

        public FormulaChoiceFormModel(FormulaSupplementChoiceDto.Detail choice)
        {
            MinQuantity = choice.MinQuantity;
            options.AddRange(choice.SupplementsToChoose.Select(supplementToChoose =>
            {
                int initialQuantity = choice.DefaultChoice.Id == supplementToChoose.Id ? choice.MinQuantity : 0;
                return new FormulaChoiceFormItem(supplementToChoose, initialQuantity);
            }));
        }

        public class Validator : FluentValidator<FormulaChoiceFormModel>
        {
            public Validator()
            {
                RuleForEach(x => x.Options).NotNull().SetValidator(new FormulaChoiceFormItem.Validator());
                RuleFor(x => x.IsMinimumQuantityReached).Must(x => x == true).WithMessage(x => $"Gelieve uit te verschillende keuzes minimum {x.MinQuantity} eenheden te selecteren");
            }
        }
    }
}

using global::Microsoft.AspNetCore.Components;
using MudBlazor;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;

namespace Foodtruck.Client.Formulas.Components
{
	public partial class Formulas
    {
        [Inject]
        public IFormulaService FormulaService { get; set; } = default !;

        [Inject]
        public ISupplementService SupplementService { get; set; } = default !;

        private IEnumerable<FormulaDto.Detail>? formulas;
        private List<string>? names = new();
        protected override async Task OnParametersSetAsync()
        {
            var response = await FormulaService.GetAllAsync();
            formulas = response.Formulas;
            var includedSupplementNames = formulas.SelectMany(formule => formule.IncludedSupplements.Select(supplementLine => supplementLine.Supplement.Name)).ToHashSet();
            var choiceNames = formulas.SelectMany(formule => formule.Choices.Select(choice => choice.Name)).ToHashSet();
            names.AddRange(choiceNames);
            names.AddRange(includedSupplementNames);
            names.ForEach(n => Console.WriteLine(n));
        }

        private FormulaDto.Detail? CurrentSelectedFormula { get; set; }

        private void OpenDialog(FormulaDto.Detail formula)
        {
            CurrentSelectedFormula = formula; // temp

            var parameters = new DialogParameters<FormulaDialog>
            {
                {
                    dialog => dialog.Formula,
                    formula
                }
            };
            DialogService.Show<FormulaDialog>($"{formula.Title} aanpassen", parameters);
        }
    }
}
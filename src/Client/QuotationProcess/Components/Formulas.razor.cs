using Microsoft.AspNetCore.Components;
using MudBlazor;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Foodtruck.Client.QuotationProcess.Helpers;

namespace Foodtruck.Client.QuotationProcess.Components
{
    public partial class Formulas
    {
        [Inject]
        public IFormulaService FormulaService { get; set; } = default!;

        [Inject]
        public ISupplementService SupplementService { get; set; } = default!;

        [Inject]
        public QuotationProcessState QuotationProcessState { get; set; } = default!;


        private IEnumerable<FormulaDto.Detail>? formulas;
        private List<string>? formulaSupplementNames = new();

        protected override async Task OnParametersSetAsync()
        {
            var response = await FormulaService.GetAllAsync();
            formulas = response.Formulas;
            var includedSupplementNames = formulas.SelectMany(formule => formule.IncludedSupplements.Select(supplementLine => supplementLine.Supplement.Name));
            var choiceNames = formulas.SelectMany(formule => formule.Choices.Select(choice => choice.Name));
            var allNames = includedSupplementNames.Concat(choiceNames);

            formulaSupplementNames.AddRange(choiceNames.ToHashSet());
            formulaSupplementNames.AddRange(includedSupplementNames.ToHashSet());

            formulaSupplementNames = formulaSupplementNames.OrderByDescending(uniqueName => allNames.Count(name => name == uniqueName)).ToList();
            formulaSupplementNames.ForEach(n => Console.WriteLine(n));

            bool useTestData = false;
            if (useTestData)
            {
                ChooseFormula(formulas.ElementAt(2));
            }
        }

        private void ChooseFormula(FormulaDto.Detail formula)
        {
            QuotationProcessState.SetupFormulaChoiceFormModels(formula);

            if (QuotationProcessState.ChoiceFormModels.Count > 0)
            {
                OpenChoicesDialog(formula);
            }
            else
            {
                QuotationProcessState.ConfigureQuotationFormula(formula);
            }
        }

        private void OpenChoicesDialog(FormulaDto.Detail formula)
        {
            var parameters = new DialogParameters<FormulaDialog>();
            parameters.Add(dialog => dialog.Formula, formula);
            parameters.Add(dialog => dialog.OnSubmit, StateHasChanged);

            DialogService.Show<FormulaDialog>($"{formula.Title} aanpassen", parameters);
        }
    }
}




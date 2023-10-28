using Microsoft.AspNetCore.Components;
using MudBlazor;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Foodtruck.Client.QuotationProcess.Components;
using Foodtruck.Client.QuotationProcess.Helpers;

namespace Foodtruck.Client.QuotationProcess
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

        private void ChooseFormula(FormulaDto.Detail formula)
        {
            if (formula.Choices.Count() != 0)
            {
                OpenDialog(formula);
            }
            else
            {
                QuotationProcessState.ConfigureFormula(formula);
            }
        }




        private void OpenDialog(FormulaDto.Detail formula)
        {
            var parameters = new DialogParameters<FormulaDialog>();
            parameters.Add(dialog => dialog.Formula, formula);
            parameters.Add(dialog => dialog.OnSubmit, StateHasChanged);

            DialogService.Show<FormulaDialog>($"{formula.Title} aanpassen", parameters);
        }
    }
}
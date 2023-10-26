using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;


namespace Foodtruck.Client.Formulas.Components
{
    public partial class Formulas
    {
        [Inject] public IFormulaService FormulaService { get; set; } = default!;
        [Inject] public ISupplementService SupplementService { get; set; } = default!;

        private IEnumerable<SupplementDto.Detail>? supplements;
        private IEnumerable<FormulaDto.Detail>? formulas;
        private ISet<string>? names;

        protected override async Task OnParametersSetAsync()
        {
            var response1 = await SupplementService.GetAllAsync();
            supplements = response1.Supplements;
            names = supplements?.Select(s => s.Name).ToHashSet();

            var response = await FormulaService.GetAllAsync();
            formulas = response.Formulas;

        }

        private void OpenDialog(FormulaDto.Detail formula)
        {
            var parameters = new DialogParameters<FormulaDialog>
            {
                { dialog => dialog.Formula, formula }
            };
            DialogService.Show<FormulaDialog>($"{formula.Title} aanpassen", parameters);
        }
    }
}
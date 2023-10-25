using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Components;

namespace Foodtruck.Client.Pages;

public partial class FormulasOverview
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
}

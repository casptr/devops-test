using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Components;

namespace Foodtruck.Client.Pages;

public partial class FormulasOverview
{
    [Inject] public IFormulaService FormulaService { get; set; } = default!;
    [Inject] public ISupplementService SupplementService { get; set; } = default!;

    private IEnumerable<FormulaDto.Detail>? formulas;
    private List<string>? names=new();

    protected override async Task OnParametersSetAsync()
    {

        var response = await FormulaService.GetAllAsync();
        formulas = response.Formulas;

        var includedSupplementNames = formulas.SelectMany(formule => formule.IncludedSupplements.Select(supplementLine => supplementLine.Supplement.Name)).ToHashSet();
        var choiceNames = formulas.SelectMany(formule => formule.Choices.Select(choice => choice.Name)).ToHashSet();
        names.AddRange(choiceNames);
        names.AddRange(includedSupplementNames);
        names.ForEach(n=>Console.WriteLine(n));

    }
}

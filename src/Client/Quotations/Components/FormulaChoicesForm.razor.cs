using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Foodtruck.Client.Quotations.Index;


namespace Foodtruck.Client.Quotations;

public partial class FormulaChoicesForm
{
    [Parameter]
    public FormulaChoicesModel Model { get; set; }
    [Inject] public IFormulaService FormulaService { get; set; } = default!;
    [Inject] public ISupplementService SupplementService { get; set; } = default!;

    private IEnumerable<FormulaDto.Detail>? formulas;
    private List<string>? names=new();

    private int numberOfGuests;

    private IEnumerable<FormulaSupplementChoiceDto.Detail> selectedFormulaChoices = new List<FormulaSupplementChoiceDto.Detail>();
    private IEnumerable<SupplementDto.Detail>? chosenSupplements = new List<SupplementDto.Detail>();

    protected override async Task OnParametersSetAsync()
    {
        // numberOfGuests = Model.NumberOfGuests;
        var response = await FormulaService.GetAllAsync();
        formulas = response.Formulas;

        var includedSupplementNames = formulas.SelectMany(formule => formule.IncludedSupplements.Select(supplementLine => supplementLine.Supplement.Name)).ToHashSet();
        var choiceNames = formulas.SelectMany(formule => formule.Choices.Select(choice => choice.Name)).ToHashSet();
        names.AddRange(choiceNames);
        names.AddRange(includedSupplementNames);
        // names.ForEach(n=>Console.WriteLine(n));

        
        if(formulas != null){
            FormulaDto.Detail defaultFormula = formulas.First();
            if(defaultFormula != null){
            HandleSelectFormula(defaultFormula.Id);
            }
        }
    }

    private FormulaDto.Detail? selectedFormula;

private void HandleSelectFormula(int formulaId)
{
    if (formulas != null)
    {
        FormulaDto.Detail? newSelectedFormula = formulas.FirstOrDefault(f => f.Id == formulaId);
        if (newSelectedFormula != null)
        {
            if (selectedFormula == null || selectedFormula.Id != formulaId)
            {
                selectedFormula = newSelectedFormula;
                selectedFormulaChoices = new List<FormulaSupplementChoiceDto.Detail>();
                foreach(var supplementToChoose in selectedFormulaChoices){
                    var appendedChosenSupplements = Model.ChosenSupplements?.Append(supplementToChoose.DefaultChoice);
                }
                
                if (selectedFormula.Choices != null)
                {
                    foreach (var choice in selectedFormula.Choices)
                    {
                        var appendedChoices = selectedFormulaChoices.Append(choice);
                    }
                    Model.IncludedSupplements = (List<FormulaSupplementChoiceDto.Detail>)selectedFormula.Choices;
                }
            }
        }
    }
}

private Dictionary<int, string> choiceNames = new Dictionary<int, string>();
private void HandleSelectSupplementChoice(int choicesId, int supplementChoiceId)
{
    if (selectedFormula != null && selectedFormula.Choices != null)
    {
        var selectedChoice = selectedFormula.Choices.First(c => c.Id == choicesId);

        if (selectedChoice.SupplementsToChoose != null)
        {
            var selectedSupplement = selectedChoice.SupplementsToChoose.FirstOrDefault(s => s.Id == supplementChoiceId);

            if (selectedSupplement != null)
            {
                choiceNames[choicesId] = selectedSupplement.Name;
            }
        }
    }
}
    private void IncrementNumberOfGuests(){
        Console.WriteLine("Incrementing");
        Model.NumberOfGuests += 1;
        Console.WriteLine(Model.NumberOfGuests);
    }

    private void DecrementNumberOfGuests(){
        if(Model.NumberOfGuests > 1){
            Model.NumberOfGuests -= 1;
        }
    }
}
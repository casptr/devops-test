using Foodtruck.Shared.Supplements;

namespace Foodtruck.Shared.Formulas;

public interface IFormulaService
{
    Task<FormulaDto.Detail> GetDetailAsync(int formulaId);
    Task<int> CreateAsync(FormulaDto.Mutate model);
    Task EditAsync(int formulaId, FormulaDto.Mutate model);
    Task DeleteAsync(int formulaId);
    Task<FormulaResult.Index> GetAllAsync();
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Formulas;

public interface IFormulaService
{
    Task<FormulaDto.Detail> GetDetailAsync(int formulaId);
    Task<int> CreateAsync(FormulaDto.Mutate model);
    Task EditAsync(int formulaId, FormulaDto.Mutate model);
    Task DeleteAsync(int formulaId);
}

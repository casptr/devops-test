using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodtruck.Shared.Formulas;

namespace Services.Formulas;
public class FormulaService : IFormulaService
{
    public Task<int> CreateAsync(FormulaDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int formulaId)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(int formulaId, FormulaDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task<FormulaDto.Detail> GetDetailAsync(int formulaId)
    {
        throw new NotImplementedException();
    }
}

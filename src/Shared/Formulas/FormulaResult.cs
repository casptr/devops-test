using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodtruck.Shared.Formulas;

namespace Foodtruck.Shared.Supplements;

public abstract class FormulaResult
{
    public class Index
    {
        public IEnumerable<FormulaDto.Detail>? Formulas { get; set; }
        public int TotalAmount { get; set; }
    }
}

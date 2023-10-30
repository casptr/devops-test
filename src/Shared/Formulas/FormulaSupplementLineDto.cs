using Foodtruck.Shared.Supplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Formulas
{
    public abstract class FormulaSupplementLineDto
    {
        public class Detail
        {
            public int Id { get; set; }
            public SupplementDto.Detail? Supplement { get; set; }
            public int Quantity { get; set; }
        }
    }
}

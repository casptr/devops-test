using Foodtruck.Shared.Supplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Formulas
{
    public abstract class FormulaSupplementChoiceDto
    {
        public class Detail
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int MinQuantity { get; set; }
            public SupplementDto.Detail? DefaultChoice { get; set; }
            public IEnumerable<SupplementDto.Detail>? SupplementsToChoose { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Supplements;

public abstract class SupplementResult
{
    public class Index
    {
        public IEnumerable<SupplementDto.Detail>? Supplements { get; set; }
        public int TotalAmount { get; set; }
    }
}

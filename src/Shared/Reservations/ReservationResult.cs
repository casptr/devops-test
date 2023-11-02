using Foodtruck.Shared.Supplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Reservations
{
    public abstract class ReservationResult
    {
        public class Index
        {
            public IEnumerable<ReservationDto.Index>? Reservations { get; set; }
            public int TotalAmount { get; set; }
        }

    }
}

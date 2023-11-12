using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Reservations
{
    public class ReservationRequest
    {
        public class Index
        {
            public DateTime FromDate { get; set; } = DateTime.UtcNow.Date;
            public DateTime? ToDate { get; set; }
            public StatusDto[] Statuses { get; set; } = { StatusDto.BEVESTIGD, StatusDto.BETAALD };
            public int Page { get; set; } = 1;
            public int PageSize { get; set; } = 25;
        }

        public class Detailed
        {
            public DateTime FromDate { get; set; } = DateTime.UtcNow.Date;
            public DateTime? ToDate { get; set; }
            public StatusDto[] Statuses { get; set; } = { StatusDto.BEVESTIGD, StatusDto.BETAALD };
            public int Page { get; set; } = 1;
            public int PageSize { get; set; } = 25;
        }
    }
}

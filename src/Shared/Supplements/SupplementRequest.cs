using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Supplements;

public abstract class SupplementRequest
{

    public class Index
    {
        public string? Searchterm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25; // Wat houdt PageSize in - Hoeveel elements of hoeveel pages?
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string? Category { get; set; }

        public int? MinAvailableAmount { get; set; }
        public int? MaxAvailableAmount { get; set; }
    }

}


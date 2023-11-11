using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;

namespace Foodtruck.Shared.Quotations;

public abstract class QuotationRequest
{
    public class Index
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
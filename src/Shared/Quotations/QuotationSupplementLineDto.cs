using Domain.Common;

namespace Foodtruck.Shared.Quotations;

public class QuotationSupplementLineDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal SupplementPrice { get; set; }
    public decimal SupplementVat { get; set; }
    public int Quantity { get; set; }
}
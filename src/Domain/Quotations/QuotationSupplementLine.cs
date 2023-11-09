using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Formulas;
using Domain.Supplements;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Quotations;

public class QuotationSupplementLine : Entity
{
    public int SupplementId { get; } = default!;
    public string Name { get; } = default!;
    public string Description { get; } = default!;
    public Category Category { get; } = default!;
    public Money Price { get; } = default!;
    public Money Vat { get; } = default!;
    public int Quantity { get; set; } = default!;

    /// <summary>
    /// Database Constructor
    /// </summary>
    private QuotationSupplementLine() { }

    public QuotationSupplementLine(SupplementItem supplementItem)
    {
        Guard.Against.Null(supplementItem, nameof(supplementItem));
        Supplement supplement = supplementItem.Supplement;
        SupplementId = supplement.Id;
        Name = supplement.Name;
        Description = supplement.Description;
        Category = supplement.Category;
        Price = supplement.Price.Copy();
        Vat = new Money(supplement.Price.Value * new decimal(supplement.Category.Vat) / 100M);
        Quantity = supplementItem.Quantity;
    }
}

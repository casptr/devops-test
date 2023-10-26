using Domain.Quotations;

namespace Foodtruck.Shared.Quotations;

public interface IQuotationService
{
    Task<int> CreateAsync(QuotationDto.Create model);
}
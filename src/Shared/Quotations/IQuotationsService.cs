using Domain.Quotations;

namespace Foodtruck.Shared.Quotations;

public interface IQuotationService
{
    Task<int> CreateAsync(QuotationDto.Create model);
    Task<QuotationDto.Detail> GetDetailAsync(int quotationId);
}
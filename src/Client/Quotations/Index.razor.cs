using Microsoft.AspNetCore.Components;
using Foodtruck.Shared.Quotations;

namespace Quotations;

public partial class Index
{
    [Inject] public IQuotationService QuotationService {get; set;} = default!;

}
using Microsoft.AspNetCore.Components;
using Foodtruck.Shared.Quotations;
using MudBlazor;
using static Foodtruck.Shared.Quotations.QuotationDto;

namespace Foodtruck.Client.Quotations;

public partial class Index
{
    [Inject] public IQuotationService QuotationService {get; set;} = default!;

    private RequestForQuotation? requestForQuotation;





}


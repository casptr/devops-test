namespace Foodtruck.Client.Pages;

using System.Net.Http.Json;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Components;
using MudBlazor;

public partial class SupplementsOverview
{
    [Inject] public ISupplementService SupplementService { get; set; } = default!;
    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }
    [Parameter, SupplyParameterFromQuery] public decimal? MinPrice { get; set; }
    [Parameter, SupplyParameterFromQuery] public decimal? MaxPrice { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? MinAvailable { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? MaxAvailable { get; set; }
    [Parameter, SupplyParameterFromQuery] public string? Category { get; set; }


    private IEnumerable<SupplementDto.Detail>? supplements;

    protected override async Task OnParametersSetAsync()
    {
        SupplementRequest.Index request = new()
        {
            Searchterm = Searchterm,
            Page = Page.HasValue ? Page.Value : 1,
            PageSize = PageSize.HasValue ? PageSize.Value : 25,
            MinPrice = MinPrice,
            MaxPrice = MaxPrice,
            MinAvailableAmount = MinAvailable,
            MaxAvailableAmount = MaxAvailable,
            Category = Category,
        };

        var response = await SupplementService.GetAllAsync();
        supplements = (IEnumerable<SupplementDto.Detail>?)response;
    }

}

namespace Foodtruck.Client.Pages;

using System.Net.Http.Json;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Components;
using MudBlazor;

public partial class SupplementsOverview
{
    [Inject] public ISupplementService SupplementService { get; set; } = default!;
  
    private IEnumerable<SupplementDto.Detail>? supplements;

    protected override async Task OnParametersSetAsync()
    {
        var response = await SupplementService.GetAllAsync();
        supplements = response.Supplements;

        foreach(var supp in supplements)
        {
            await SupplementService.AddImage(supp.Id);
        }
       

        response = await SupplementService.GetAllAsync();
        supplements = response.Supplements;
    }

}

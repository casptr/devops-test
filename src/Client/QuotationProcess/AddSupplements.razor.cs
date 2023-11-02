using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Foodtruck.Client;
using Foodtruck.Client.Shared;
using Foodtruck.Client.Layout;
using MudBlazor;
using Foodtruck.Client.QuotationProcess.Components;
using Foodtruck.Client.QuotationProcess.Helpers;
using Foodtruck.Shared.Supplements;

namespace Foodtruck.Client.QuotationProcess
{
    public partial class AddSupplements
    {
        [Inject]
        public ISupplementService SupplementService { get; set; } = default !;

        private IEnumerable<SupplementDto.Detail>? supplements;
        protected override async Task OnParametersSetAsync()
        {
            var response = await SupplementService.GetAllAsync();
            supplements = response.Supplements;
        }
    }
}
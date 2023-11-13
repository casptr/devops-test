using Foodtruck.Shared.Supplements;
using global::Microsoft.AspNetCore.Components;

namespace Foodtruck.Client.Supplements.Components
{
    public partial class Supplements
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
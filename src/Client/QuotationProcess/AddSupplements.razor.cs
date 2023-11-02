using global::Microsoft.AspNetCore.Components;
using Foodtruck.Shared.Supplements;
using Foodtruck.Client.QuotationProcess.Helpers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Foodtruck.Client.QuotationProcess
{
	public partial class AddSupplements
    {
        [Inject] public QuotationProcessState QuotationProcessState { get; set; } = default!;
		[Inject] public ISupplementService SupplementService { get; set; } = default !;

        private IEnumerable<SupplementChoice>? supplements;
        protected override async Task OnParametersSetAsync()
        {
            var response = await SupplementService.GetAllAsync();
            supplements = response.Supplements?.Select( supplement => new SupplementChoice()
            {
                Supplement = supplement,
                Quantity = 0
            }).ToList();
        }

        private void AddSupplement(SupplementDto.Detail supplement, int quantity)
        {
            var supplementChoice = new SupplementChoice()
            {
                Supplement = supplement,
                Quantity = quantity
            };

            QuotationProcessState.AddSupplement(supplementChoice);
        }
    }
}
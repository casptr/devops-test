using global::Microsoft.AspNetCore.Components;
using Foodtruck.Shared.Supplements;
using Foodtruck.Client.QuotationProcess.Helpers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace Foodtruck.Client.QuotationProcess
{
    public partial class AddSupplements
    {
        [Inject] public QuotationProcessState QuotationProcessState { get; set; } = default!;
        [Inject] public ISupplementService SupplementService { get; set; } = default!;

        private IEnumerable<ExtraSupplement>? supplements;
        protected override async Task OnParametersSetAsync()
        {
            var response = await SupplementService.GetAllAsync();
            supplements = response.Supplements?.Select(supplement =>
            {
                ExtraSupplement? extraSupplement = QuotationProcessState.SupplementChoices.Where(supplementInState => supplementInState.Equals(supplement)).FirstOrDefault();
                return new ExtraSupplement()
                {
                    Supplement = supplement,
                    Quantity = extraSupplement is null ? 0 : extraSupplement.Quantity,
                };
            }).ToList();
        }

        private void AddSupplement(SupplementDto.Detail supplement, int quantity)
        {
            var supplementChoice = new ExtraSupplement()
            {
                Supplement = supplement,
                Quantity = quantity
            };

            QuotationProcessState.AddSupplement(supplementChoice);
        }
    }
}
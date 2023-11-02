using Microsoft.AspNetCore.Components;
using MudBlazor;
using Foodtruck.Client.QuotationProcess.Helpers;
namespace Foodtruck.Client.QuotationProcess
{
    public partial class CustomerDetails
    {
        [Inject] public QuotationProcessState QuotationProcessState { get; set; } = default!;
        private CustomerDetailsFormModel model => QuotationProcessState.CustomerDetailsFormModel;
        private readonly CustomerDetailsFormModel.Validator validator = new();

        private MudForm form = default !;
       
        bool useTestData = false;

        // For testing purposes TODO delete this
        protected override void OnInitialized()
        {
            if (!useTestData)
                return;
            model.Customer.WantsMarketingMails = true;
            model.Customer.Firstname = "Bart";
            model.Customer.Lastname = "Vandenbranden";
            model.Customer.CompanyName = "Vandenbranden Corp";
            model.Customer.CompanyNumber = "123.345.278";
            model.Customer.Email = "bvandenbranden@vbcorp.be";
            model.Customer.Phone = "0470454372";
            model.EventAddress.Street = "Sint-Denijslaan";
            model.EventAddress.HouseNumber = "230";
            model.EventAddress.City = "Gent";
            model.EventAddress.Zip = "9000";
            model.BillingAddress.Street = "Berkenlaan";
            model.BillingAddress.HouseNumber = "45";
            model.BillingAddress.City = "Melle";
            model.BillingAddress.Zip = "9090";
            model.IsEventAddressDifferentThanBillingAddress = true;
            model.ExtraInfo = "Het gaat om een bedrijfsfeest. Je mag mij gerust bellen om de offerte te bespreken.";
        }

        public async Task Submit()
        {
            await form.Validate();

            if (!form.IsValid)
                return;

            QuotationProcessState.ConfigureQuotationCustomerDetails();
            QuotationProcessState.PrintQuotation();
        }
    }
}
using Foodtruck.Shared.Emails;
using Foodtruck.Shared.Quotations;
using Foodtruck.Shared.Supplements;
using System.Net.Http.Json;

namespace Foodtruck.Client.Admin;

public class EmailService : IEmailService
{
    private readonly HttpClient client;
    private const string endpoint = "api/email";

    public EmailService(HttpClient client)
    {
        this.client = client;
    }

    public Task<bool> SendNewQuotationConfirmationToCustomer(QuotationDto.Detail quotation)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SendNewQuotationPdfToAdmin(QuotationDto.Detail quotation)
    {
        throw new NotImplementedException();
    }
}

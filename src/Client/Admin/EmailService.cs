using Foodtruck.Shared.Emails;
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
    public Task<bool> SendEmail(string text)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SendQuotationPdfEmail(int quotationId)
    {
        var response = await client.GetAsync($"{endpoint}/{quotationId}"); 
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendQuotationPdfEmailController(string base64, string text)
    {
        throw new NotImplementedException();
    }
}

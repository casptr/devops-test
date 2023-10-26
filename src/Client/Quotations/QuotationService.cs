using System.Net;
using System.Net.Http.Json;
using Domain.Quotations;
using Foodtruck.Shared.Quotations;
using Client.Extensions;

namespace Foodtruck.Client.Quotations;

public class QuotationService : IQuotationService
{
    private readonly HttpClient client;
    private const string endpoint = "api/Quotation";
    public QuotationService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<int> CreateAsync(QuotationDto.Mutate request)
    {
        var response = await client.PostAsJsonAsync(endpoint, request);
        return await response.Content.ReadFromJsonAsync<int>();
    }
}
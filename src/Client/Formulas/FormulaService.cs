using System.Net;
using System.Net.Http.Json;
using Client.Extensions;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;

namespace Foodtruck.Client.Formulas;

public class FormulaService : IFormulaService
{
    private readonly HttpClient client;
    private const string endpoint = "api/formula";
    public FormulaService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<FormulaResult.Index> GetAllAsync()
    {
        var response = await client.GetFromJsonAsync<FormulaResult.Index>($"{endpoint}/all");
        return response!;
    }

    public async Task<FormulaDto.Detail> GetDetailAsync(int formulaId)
    {
        var response = await client.GetFromJsonAsync<FormulaDto.Detail>($"{endpoint}/{formulaId}");
        return response;
    }

    //public async Task<int> CreateAsync(FormulaDto.Mutate request)
    //{
    //    var response = await client.PostAsJsonAsync(endpoint, request);
    //    return await response.Content.ReadFromJsonAsync<int>();
    //}

    //public async Task DeleteAsync(int formulaId)
    //{
    //    await client.DeleteAsync($"{endpoint}/{formulaId}");
    //}

    //public async Task EditAsync(int formulaId, FormulaDto.Mutate model)
    //{
    //    var response = await client.PutAsJsonAsync($"{endpoint}/{formulaId}", model);
    //}



    //public async Task AddFormulaSupplementLine(int formulaId)
    //{
    //    await client.PostAsJsonAsync($"{endpoint}/{formulaId}/addsuppline",formulaId);
    //}

    //public async Task AddFormulaSupplementChoice(int formulaId)
    //{
    //    await client.PostAsJsonAsync($"{endpoint}/{formulaId}/addsuppchoice", formulaId);
    //}
}

using Client.Extensions;
using Foodtruck.Shared.Reservations;
using Foodtruck.Shared.Supplements;
using System.Net.Http.Json;

namespace Foodtruck.Client.QuotationProcess
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient client;
        private const string endpoint = "api/reservation";

        public ReservationService(HttpClient client)
        {
            this.client = client;
        }

        // TODO query params
        public async Task<ReservationResult.Index> GetIndexAsync(ReservationRequest.Index request)
        {
            var response = await client.GetFromJsonAsync<ReservationResult.Index>($"{endpoint}?{request.AsQueryString()}");
            return response!;
        }

        public async Task<ReservationResult.Detailed> GetDetailedAsync(ReservationRequest.Detailed request)
        {
            var response = await client.GetFromJsonAsync<ReservationResult.Detailed>($"{endpoint}/detailed?{request.AsQueryString()}");
            return response!;
        }
    }
}

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

        public async Task<ReservationResult.Index> GetIndexAsync()
        {
            var response = await client.GetFromJsonAsync<ReservationResult.Index>($"{endpoint}");
            return response!;
        }
    }
}

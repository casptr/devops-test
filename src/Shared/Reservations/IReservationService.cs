namespace Foodtruck.Shared.Reservations
{
    public interface IReservationService
    {
        Task<ReservationResult.Index> GetIndexAsync(ReservationRequest.Index request);

        Task<ReservationResult.Detailed> GetDetailedAsync(ReservationRequest.Detailed request);
    }
}

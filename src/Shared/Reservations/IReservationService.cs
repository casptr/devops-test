namespace Foodtruck.Shared.Reservations
{
    public interface IReservationService
    {
        Task<ReservationResult.Index> GetIndexAsync();

        Task<ReservationResult.Detailed> GetDetailedAsync();
    }
}

using Bogus;
using Foodtruck.Shared.Reservations;

namespace Services.Reservations
{
    public class FakeReservationService : IReservationService
    {
        private readonly List<ReservationDto.Index> reservations = new();

        public FakeReservationService()
        {
            var reservationIds = 0;
            ReservationDto.Index? prev = null;

            Random random = new Random();
            var reservationFaker = new Faker<ReservationDto.Index>("nl")
            .UseSeed(1)
            .RuleFor(x => x.Id, _ => ++reservationIds)
            .RuleFor(x => x.Start, f => prev?.End.AddDays(random.Next(1, 30)) ?? DateTime.Now.AddDays(1))
            .RuleFor(x => x.End, (f, current) => current.Start.AddDays(random.Next(0, 5))).FinishWith((f, current) => prev = current);

            reservations = reservationFaker.Generate(25);
        }

        public Task<ReservationResult.Index> GetIndexAsync()
        {
            return Task.FromResult(new ReservationResult.Index()
            {
                Reservations = reservations.AsEnumerable(),
                TotalAmount = reservations.Count,
            });
        }
    }
}

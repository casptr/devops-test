using Bogus;
using Foodtruck.Shared.Reservations;

namespace Services.Reservations
{
    public class FakeReservationService : IReservationService
    {
        private readonly List<ReservationDto.Detail> reservations = new();

        public FakeReservationService()
        {
            var reservationIds = 0;
            ReservationDto.Detail? prev = null;

            Random random = new Random();
            var reservationFaker = new Faker<ReservationDto.Detail>("nl")
            .UseSeed(1)
            .RuleFor(x => x.Id, _ => ++reservationIds)
            .RuleFor(x => x.Start, f => prev?.End.AddDays(random.Next(1, 30)) ?? DateTime.Now.AddDays(1))
            .RuleFor(x => x.Description, f => f.Person.FullName)
            .RuleFor(x => x.End, (f, current) => current.Start.AddDays(random.Next(0, 5))).FinishWith((f, current) => prev = current);

            reservations = reservationFaker.Generate(25);
        }

        public Task<ReservationResult.Index> GetIndexAsync()
        {
            return Task.FromResult(new ReservationResult.Index()
            {
                Reservations = reservations.Select(reservationDetail => new ReservationDto.Index() { Id = reservationDetail.Id, Start = reservationDetail.Start, End = reservationDetail.End }).AsEnumerable(),
                TotalAmount = reservations.Count,
            });
        }

        public Task<ReservationResult.Detailed> GetDetailedAsync()
        {
            return Task.FromResult(new ReservationResult.Detailed()
            {
                Reservations = reservations.AsEnumerable(),
                TotalAmount = reservations.Count,
            });
        }

    }
}

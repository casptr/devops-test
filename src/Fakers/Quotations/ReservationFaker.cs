using Domain.Common;
using Domain.Quotations;
using Fakers.Common;

namespace Fakers.Quotations;

public class ReservationFaker : EntityFaker<Reservation>
{
	private static int id = 100;
	public ReservationFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Reservation(DateTime.Now.AddDays(8), DateTime.Now.AddDays(f.Random.Int(8,15)), f.Company.CatchPhrase(), f.Random.Int(0,1) == 0 ? Status.VOORGESTELD : f.Random.Int(0, 1) == 1 ? Status.BETAALD : Status.BEVESTIGD)).RuleFor(f => f.Id, s => id++);
	}
}

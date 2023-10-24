using Domain.Common;
using Domain.Quotations;
using Fakers.Common;

namespace Fakers.Quotations;

public class ReservationFaker : EntityFaker<Reservation>
{
	public ReservationFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Reservation(new DateTime().AddDays(8), new DateTime().AddDays(f.Random.Int(8,15)), f.Company.CatchPhrase(), f.Random.Int(0,1) == 0 ? Status.VOORGESTELD : f.Random.Int(0, 1) == 1 ? Status.BETAALD : Status.BEVESTIGD));
	}
}

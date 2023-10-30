using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Formulas;

public class Foodtruck : Entity
{
    // 1 dag huren: 350 excl 21% btw 
    // 2 dagen huren: 450€ excl 21% btw
    // 3 dagen huren: 520€ excl 21%
    // Elke extra dag: +50€ excl 21% btw
    private readonly List<PricePerDayLine> pricePerDays = new();
    public IReadOnlyCollection<PricePerDayLine> PricePerDays => pricePerDays.AsReadOnly();

    private Money extraPricePerDay = default!;
    public Money ExtraPricePerDay { get => extraPricePerDay; set => extraPricePerDay = Guard.Against.Null(value, nameof(ExtraPricePerDay)); } // ex. 50€ excl 21%btw

    // TODO move this code?
    public Money CalculatePrice(int numberOfDays)
    {
        PricePerDayLine? pricePerDay = PricePerDays.SingleOrDefault(day => day.DayNumber == numberOfDays);
        if (pricePerDay is not null)
            return pricePerDay.Price;


        PricePerDayLine lastPricePerDay = PricePerDays.OrderByDescending(day => day.DayNumber).First();
        int dayDifference = numberOfDays - lastPricePerDay.DayNumber;
        return new Money(lastPricePerDay.Price.Value + (dayDifference * ExtraPricePerDay.Value));
    }
    public Foodtruck() { }
    /*private Foodtruck() { }*/
}

// DayNumber Price
// 1          350
// 2          450
public class PricePerDayLine : Entity
{
    private int dayNumber = default!;
    public int DayNumber { get => dayNumber; set => dayNumber = Guard.Against.NegativeOrZero(value, nameof(DayNumber)); }

    private Money price = default!;
    public Money Price { get => price; set => price = Guard.Against.Null(value, nameof(Price)); }

    private PricePerDayLine() { }

    public PricePerDayLine(int dayNumber, Money price)
    {
        DayNumber = dayNumber;
        Price = price;
    }


}

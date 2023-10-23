using Domain.Common;

namespace Domain.Formulas
{
    public class Foodtruck : Entity
    {
        // 1 dag huren: 350 excl 21% btw 
        // 2 dagen huren: 450€ excl 21% btw
        // 3 dagen huren: 520€ excl 21%
        // Elke extra dag: +50€ excl 21% btw

        private readonly List<PricePerDay> pricePerDays = new();
        public IReadOnlyCollection<PricePerDay> PricePerDays => pricePerDays.AsReadOnly();
        public Money ExtraPricePerDay { get; set; } // 50€ excl 21%btw

        public Money CalculatePrice(int numberOfDays)
        {
            PricePerDay? pricePerDay = PricePerDays.SingleOrDefault(day => day.DayNumber == numberOfDays);
            if (pricePerDay != null)
                return pricePerDay.Price;


            PricePerDay lastPricePerDay = PricePerDays.OrderByDescending(day => day.DayNumber).First();
            int dayDifference = numberOfDays - lastPricePerDay.DayNumber;
            return new Money(lastPricePerDay.Price.Value + (dayDifference * ExtraPricePerDay.Value));
        }
    }

    // DayNumber Price
    // 1          350
    // 2          450
    public class PricePerDay
    {
        public int DayNumber { get; set; }
        public Money Price { get; set; }
    }
}

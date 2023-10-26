namespace Foodtruck.Shared.Reservations;

public abstract class ReservationDto
{
    public class Index
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class Create
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string? Description { get; set; }
    }
}

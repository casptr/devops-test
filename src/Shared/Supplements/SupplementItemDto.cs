namespace Foodtruck.Shared.Supplements
{
    public abstract class SupplementItemDto
    {
        public class Create
        {
            public int SupplementId { get; set; }
            public int Quantity { get; set; }
        }
 
    }
}

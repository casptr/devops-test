using Foodtruck.Shared.Supplements;

namespace Foodtruck.Client.QuotationProcess.Helpers;

public class ExtraSupplement
{
	public SupplementDto.Detail Supplement { get; set; } = default!;
    public int Quantity { get; set; }

    // override object.Equals
    public override bool Equals(object obj)
    {
        ExtraSupplement other = obj as ExtraSupplement;

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        } else
        {
            return Supplement.Name == other!.Supplement.Name;
        }
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return Supplement.Name.GetHashCode();
    }
}

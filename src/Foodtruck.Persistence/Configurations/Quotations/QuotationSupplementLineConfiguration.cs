using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foodtruck.Persistence.Configurations.Quotations;

internal class QuotationSupplementLineConfiguration : IEntityTypeConfiguration<QuotationSupplementLine>
{
	public void Configure(EntityTypeBuilder<QuotationSupplementLine> builder)
	{
		builder.OwnsOne(x => x.Price).Property(x => x.Value).HasColumnName(nameof(QuotationSupplementLine.Price));
        builder.OwnsOne(x => x.Vat).Property(x => x.Value).HasColumnName(nameof(QuotationSupplementLine.Vat));
    }
}
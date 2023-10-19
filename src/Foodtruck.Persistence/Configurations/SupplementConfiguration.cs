using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BogusStore.Persistence.Configurations.Customers;

internal class SupplementConfiguration : IEntityTypeConfiguration<Supplement>
{
    public void Configure(EntityTypeBuilder<Supplement> builder)
    {
        builder.OwnsOne(x => x.Price).Property(x => x.Value);
    }
}
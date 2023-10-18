using System.Reflection.Emit;
using System.Reflection.Metadata;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BogusStore.Persistence.Configurations.Customers;

internal class FormulaConfiguration : IEntityTypeConfiguration<Formula>
{
    public void Configure(EntityTypeBuilder<Formula> builder)
    {
        builder.OwnsOne(x => x.Price).Property(x => x.Value);
/*        builder.OwnsMany(x => x.IncludedSupplements, supplement =>
        {
            supplement.WithOwner().HasForeignKey("FormulaId");
            supplement.Property("Id");
            supplement.HasKey("Id");
        });*/
    }
}
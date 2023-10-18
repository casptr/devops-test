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
    }
}
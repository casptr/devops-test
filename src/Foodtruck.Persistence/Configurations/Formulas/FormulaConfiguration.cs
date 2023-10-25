using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foodtruck.Persistence.Configurations.Formulas;

internal class FormulaConfiguration : IEntityTypeConfiguration<Formula>
{
    public void Configure(EntityTypeBuilder<Formula> builder)
    {
        builder.OwnsOne(x => x.Price).Property(x => x.Value);
    }
}
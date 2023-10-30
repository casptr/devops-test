using Domain.Formulas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Customers;

namespace Foodtruck.Persistence.Configurations.Customers;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.OwnsOne(x => x.Email).Property(x => x.Value);
    }
}

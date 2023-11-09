using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Quotations;
using System.Reflection.Metadata;
using Domain.Customers;

namespace Foodtruck.Persistence.Configurations.Quotations
{
    internal class QuotationConfiguration : IEntityTypeConfiguration<Quotation>
    {
        public void Configure(EntityTypeBuilder<Quotation> builder)
        {
            builder.HasOne(x => x.Customer).WithOne().HasForeignKey<Customer>(c => c.QuotationId).IsRequired();

        }
    }
}

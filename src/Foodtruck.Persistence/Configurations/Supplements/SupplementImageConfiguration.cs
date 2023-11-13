using Domain.Supplements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodtruck.Persistence.Configurations.Supplements;

public class SupplementImageConfiguration : IEntityTypeConfiguration<SupplementImage>
{
    public void Configure(EntityTypeBuilder<SupplementImage> builder)
    {
        builder.Property(x => x.Image).HasConversion(x => x!.ToString(), x => new Uri(x)).HasColumnName(nameof(SupplementImage.Image));
    }
}

using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Foodtruck.Persistence.Configurations.Formulas;

public class FormulaSupplementChoiceConfiguration : IEntityTypeConfiguration<FormulaSupplementChoice>
{
    public void Configure(EntityTypeBuilder<FormulaSupplementChoice> builder)
    {
        builder.HasMany(e => e.SupplementsToChoose).WithMany(e => e.FormulaSupplementChoices).UsingEntity(join => join.ToTable("FormulaChoicesSupplements"));
        builder.HasOne(e => e.DefaultChoice).WithMany(e => e.FormulaSupplementDefaultChoices);
    }
}

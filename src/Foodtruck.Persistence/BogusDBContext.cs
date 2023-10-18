using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;
using BogusStore.Persistence.Triggers;
using System.Reflection.Metadata;
using BogusStore.Persistence.Configurations.Customers;

namespace Foodtruck.Persistence;

public class BogusDbContext : DbContext
{
    public DbSet<Supplement> Supplements => Set<Supplement>();
    public DbSet<Formula> Formulas => Set<Formula>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseInMemoryDatabase(databaseName: "BogusDb");
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
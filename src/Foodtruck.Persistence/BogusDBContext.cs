using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BogusStore.Persistence.Triggers;
using Domain.Supplements;
using Domain.Formulas;
using Domain.Customers;
using Domain.Quotations;

namespace Foodtruck.Persistence;

public class BogusDbContext : DbContext
{
	#region Formulas
	public DbSet<Formula> Formulas => Set<Formula>();
	public DbSet<FormulaSupplementLine> FormulaSupplementLines => Set<FormulaSupplementLine>();
	public DbSet<FormulaSupplementChoice> FormulaSupplementChoices => Set<FormulaSupplementChoice>();
	public DbSet<Domain.Formulas.Foodtruck> Foodtruck => Set<Domain.Formulas.Foodtruck>();
    public DbSet<PricePerDayLine> PricePerDayLines => Set<PricePerDayLine>();
	#endregion

	#region Supplements
	public DbSet<Supplement> Supplements => Set<Supplement>();
    public DbSet<Category> Category => Set<Category>();
    #endregion

    #region Customers
    public DbSet<Customer> Customers => Set<Customer>();
    #endregion

    #region Quotations
    public DbSet<Quotation> Quotations => Set<Quotation>();
    public DbSet<QuotationSupplementLine> QuotationSupplementLines => Set<QuotationSupplementLine>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<QuotationVersion> QuotationVersions => Set<QuotationVersion>();
	#endregion

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
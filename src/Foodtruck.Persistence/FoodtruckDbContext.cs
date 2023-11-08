using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Domain.Supplements;
using Microsoft.EntityFrameworkCore;

namespace Foodtruck.Persistence
{
    public class FoodtruckDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MarketingSubscriber> MarketingSubscribers { get; set; }

        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Quotation> Quotations { get; set; }

        public FoodtruckDbContext(DbContextOptions<FoodtruckDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodtruckDbContext).Assembly);
        }
    }
}

using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Domain.Supplements;
using Microsoft.EntityFrameworkCore;

namespace Foodtruck.Persistence
{
    public class FoodtruckDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<MarketingSubscriber> MarketingSubscriber { get; set; }

        public DbSet<Supplement> Supplement { get; set; }
        public DbSet<Formula> Formula { get; set; }
        public DbSet<Quotation> Quotation { get; set; }

        public FoodtruckDbContext(DbContextOptions<FoodtruckDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodtruckDbContext).Assembly);
        }
    }
}

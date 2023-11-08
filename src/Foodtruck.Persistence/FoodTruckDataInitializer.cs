using Domain;
using Domain.Formulas;
using Foodtruck.Persistence;
//using Foodtruck.Shared.Formulas;
//using Foodtruck.Shared.Supplements;

namespace Persistence
{
    public class FoodTruckDataInitializer
    {
        private readonly FoodtruckDbContext _dbContext;

        public FoodTruckDataInitializer(FoodtruckDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            SeedProducts();
        }

        private void SeedProducts()
        {

            //var catBier = new CategoryDto.Index { Id = 1, Name = "Bier" };
            //var catDrinken = new CategoryDto.Index { Id = 2, Name = "Eten" };
            //var catExtra = new CategoryDto.Index { Id = 3, Name = "Extra" };
            //Domain.Formulas.Foodtruck foodtruck = new Domain.Formulas.Foodtruck();

            //var formulaBasic = new Formula(
            //    foodtruck: new Domain.Formulas.Foodtruck(),
            //    title: "Basis",
            //    description: "Enkel de foodtruck huren",
            //    imageUrl: new Uri("https://imageupload.io/ib/P0NNekeu9XhNflc_1698522659.jpg")
            //);
            ////var products = new ProductFaker(hasRandomId: false).Generate(100);
            //_dbContext.Formulas.Add(formulaBasic);
            //_dbContext.SaveChanges();
        }
    }
}

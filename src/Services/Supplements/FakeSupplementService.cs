using Domain.Formulas;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Supplements;

public class FakeSupplementService : ISupplementService
{

    private readonly List<SupplementDto.Detail> supplements = new();
    public FakeSupplementService()
    {
        var catBier = new CategoryDto.Index { Id = 1, Name = "Bier" };
        var catEten = new CategoryDto.Index { Id = 2, Name = "Eten" };
        var catExtra = new CategoryDto.Index { Id = 3, Name = "Extra" };
        var catDrinken = new CategoryDto.Index { Id = 4, Name = "Drinken" };
       
        var supDienblad = new SupplementDto.Detail()
        {
            Id = 8,
            Category = catExtra,
            AmountAvailable = 10,
            Description = "Plateau’s/dienblad zwart antislip (diameter 35cm).",
            Name = "Plateau’s",
            Price = 1.5M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Dienblad.jpg") },
        };

        var supSfeerverlichting = new SupplementDto.Detail()
        {

            Id = 14,
            AmountAvailable = 8,
            Category = catExtra,
            Description = "Lichtslinger (guirlandes 10m).",
            Name = "Lichtslinger",
            Price = 3,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Sfeerverlichting.jpg") }
        };
        
        var supSaladette = new SupplementDto.Detail()
        {
            Id = 19,
            AmountAvailable = 2,
            Category = catExtra,
            Description = "Inclusief 5 GN bakken ¼ + 5 deksels, diepte 150 mm",
            Name = "Saladette",
            Price = 65M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Saladette.jpg") }
        };

        var supGNBakken = new SupplementDto.Detail()
        {
            Id = 20,
            AmountAvailable = 20,
            Category = catExtra,
            Description = "GN bakken ¼ + 5 deksels, diepte 150 mm",
            Name = "GN bakken",
            Price = 3M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/GNbaken14.jpg") }
        };
        var supGNBakken1_6 = new SupplementDto.Detail()
        {
            Id = 21,
            AmountAvailable = 10,
            Category = catExtra,
            Description = "GN bakken 1/6  + deksel, diepte 150mm ",
            Name = "GN bakken 1/6",
            Price = 3M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/GNbakken16.jpg") }
        };
        var supBarKoeler = new SupplementDto.Detail()
        {
            Id = 22,
            AmountAvailable = 1,
            Category = catExtra,
            Description = "Barkoeler 320 liter – 3x glazen schuifdeur - zwart ( 50cm (l) x 135cm (b) x 87cm (h)",
            Name = "Barkoeler",
            Price = 65M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Barkoeler.jpg") }
        };
        
        var supCocktailglasGoud = new SupplementDto.Detail()
        {
            Id = 23,
            AmountAvailable = 100,
            Category = catExtra,
            Description = "Cocktail glas met gouden rand 330ml",
            Name = "Cocktail glas gouden rand",
            Price = 0.5M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/CocktailGoudenRand.jpg") }
        };

        var supCocktailglasGewoon = new SupplementDto.Detail()
        {
            Id = 24,
            AmountAvailable = 100,
            Category = catExtra,
            Description = "Cocktail glas type rand 330ml",
            Name = "Cocktail glas type rand",
            Price = 0.2M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/CocktailRand.jpg") }
        };

        var supCocktailglasKlein = new SupplementDto.Detail()
        {
            Id = 25,
            AmountAvailable = 100,
            Category = catExtra,
            Description = "Cocktailglas 250ml",
            Name = "Cocktailglas klein",
            Price = 0.15M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Cocktailglazen.jpg") }
        };

        var supIjsemmer = new SupplementDto.Detail()
        {
            Id = 26,
            AmountAvailable = 1,
            Category = catExtra,
            Description = "Ijsemmer 7l",
            Name = "Ijsemmer",
            Price = 10M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Ijsemmer.jpg") }
        };

        var supVuurschaal = new SupplementDto.Detail()
        {
            Id = 27,
            AmountAvailable = 1,
            Category = catExtra,
            Description = "Vuurschaal diameter 120cm ",
            Name = "Vuurschaal",
            Price = 40M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Vuurschaal.jpg") }
        };

        var supDriepoot = new SupplementDto.Detail()
        {
            Id = 28,
            AmountAvailable = 1,
            Category = catExtra,
            Description = "Driepoot met BBQ rooster + vuurschaal ",
            Name = "Driepoot",
            Price = 100M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Driepoot.jpg") }
        };

        var supDiepvries = new SupplementDto.Detail()
        {
            Id = 29,
            AmountAvailable = 1,
            Category = catExtra,
            Description = "Diepvries 80l (60cm (l)x60cm (b) x 80cm (h))",
            Name = "Diepvries",
            Price = 50M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Diepvries.jpg") }
        };
        var supSnijplank = new SupplementDto.Detail()
        {
            Id = 30,
            AmountAvailable = 3,
            Category = catExtra,
            Description = "Snijplanken groen (60cm x 40cm)",
            Name = "Snijplank",
            Price = 4M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Snijplanken.jpg") }
        };
        var supSpoelbak = new SupplementDto.Detail()
        {
            Id = 31,
            AmountAvailable = 1,
            Category = catExtra,
            Description = "Spoelbak klein type camping 100cm(b) x 50 cm(l) x 80cm(h)",
            Name = "Spoelbak",
            Price = 10M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Spoelbak.jpg") }
        };
        var supDrankenDispenser = new SupplementDto.Detail()
        {
            Id = 32,
            AmountAvailable = 2,
            Category = catExtra,
            Description = "Drankendispenser",
            Name = "Drankendispenser",
            Price = 15M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Drankendispenser.jpg") }
        };
        var supSoepketel = new SupplementDto.Detail()
        {
            Id = 33,
            AmountAvailable = 2,
            Category = catExtra,
            Description = "Soepketel 10 liter + pollepel",
            Name = "Soepketel",
            Price = 15M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Soepketel.jpg") }
        };
        var supStrobaal = new SupplementDto.Detail()
        {
            Id = 34,
            AmountAvailable = 10,
            Category = catExtra,
            Description = "Strobalen (80cm (l) x 45cm (b) x 45cm (h) )",
            Name = "Strobaal",
            Price = 4M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Strobalen.jpg") }
        };
        var supSchapenvacht = new SupplementDto.Detail()
        {
            Id = 35,
            AmountAvailable = 10,
            Category = catExtra,
            Description = "Schapenvacht ong. 100 cm x 50cm",
            Name = "Schapenvacht",
            Price = 12M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Schapenvacht.jpg") }
        };
        var supBiertafel = new SupplementDto.Detail()
        {
            Id = 36,
            AmountAvailable = 5,
            Category = catExtra,
            Description = "Biertafel set : 1 tafel (220cm (l) x 80cm (b) x 77cm (h) + 2 banken ( 220cm (l) x 25cm (b) x 48 cm (h)",
            Name = "Biertafel set",
            Price = 15M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Biertafel.jpg") }
        };
        var supFruitkist = new SupplementDto.Detail()
        {
            Id = 37,
            AmountAvailable = 20,
            Category = catExtra,
            Description = "Fruitkisten (50cm (l) x 41cm (b) x 31cm (h)",
            Name = "Fruitkist",
            Price = 5M,
            ImageUrls = new List<Uri>() { new Uri("https://localhost:7143/images/Fruitkist.jpg") }
        };
        supplements.Add(supDienblad);
        supplements.Add(supSfeerverlichting);
        supplements.Add(supSaladette);
        supplements.Add(supGNBakken);
        supplements.Add(supGNBakken1_6);
        supplements.Add(supBarKoeler);

        supplements.Add(supCocktailglasKlein);
        supplements.Add(supCocktailglasGewoon);
        supplements.Add(supCocktailglasGoud);
        supplements.Add(supIjsemmer);
        supplements.Add(supVuurschaal);
        supplements.Add(supDriepoot);

        supplements.Add(supDiepvries);
        supplements.Add(supSnijplank);
        supplements.Add(supSpoelbak);
        supplements.Add(supDrankenDispenser);
        supplements.Add(supSoepketel);
        supplements.Add(supStrobaal);
        supplements.Add(supSchapenvacht);
        supplements.Add(supBiertafel);
        supplements.Add(supFruitkist);

    }

    public Task<SupplementResult.Index> GetAllAsync()
    {
        var result = new SupplementResult.Index()
        {
            Supplements = supplements,
            TotalAmount = supplements.Count
        };
        return Task.FromResult(result);
    }

    public Task AddImage(int supplementId)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateAsync(SupplementDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int supplementId)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(int supplementId, SupplementDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task<SupplementDto.Detail> GetDetailAsync(int supplementId)
    {
        throw new NotImplementedException();
    }

    public Task<SupplementResult.Index> GetIndexAsync(SupplementRequest.Index request)
    {
        throw new NotImplementedException();
    }
}

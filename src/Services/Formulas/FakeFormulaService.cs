using Bogus;
using Domain.Common;
using Domain.Quotations;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Reservations;
using Foodtruck.Shared.Supplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Formulas;

public class FakeFormulaService : IFormulaService
{
    private readonly List<FormulaDto.Detail> formulas = new();
    public FakeFormulaService()
    {
       
        var catBier = new CategoryDto.Index { Id = 1, Name = "Bier" };
        var catDrinken = new CategoryDto.Index { Id = 2, Name = "Eten" };
        var catExtra = new CategoryDto.Index { Id = 3, Name = "Extra" };

        var suppJup = new SupplementDto.Detail()
        {
            Id = 1,
            Name = "Vat Jupiler",
            Description = "1 Vat Jupiler van 50 L",
            AmountAvailable = 5,
            Price = 150,
            ImageUrls = new List<Uri>() { new Uri("https://thysshop.be/321-thickbox_default/Jupiler-33-cl-Fles.jpg") },
            Category = catDrinken,

        };

        var suppDuvel = new SupplementDto.Detail()
        {
            Id = 2,
            Name = "Vat Duvel",
            Description = "Vat Duvel van 50 L",
            AmountAvailable = 3,
            Price = 200,
            ImageUrls = new List<Uri>() { new Uri("https://www.prikentik.be/media/catalog/product/d/u/duvel_bottle_classic_be-nl-fr.jpg") },
            Category = catDrinken,
        };
        var suppStella = new SupplementDto.Detail()
        {
            Id = 3,
            Name = "Vat Stella Artois",
            Description = "1 Vat Stella van 50 L",
            AmountAvailable = 7,
            Price = 190M,
            ImageUrls = new List<Uri>() { new Uri("https://goedkoopdrank.be/wp-content/uploads/2023/07/P2911.jpg") },
            Category = catDrinken,
        };

        var suppHamburger = new SupplementDto.Detail()
        {
            Id = 4,
            Name = "Hamburgers",
            Description = "Heerlijke rundshamburger",
            AmountAvailable = 70,
            Price = 4,
            ImageUrls = new List<Uri>() { new Uri("https://www.fryskblackangus.nl/wp-content/uploads/2019/05/black-angus-steakburger-bbq-1080x675.jpg") },
            Category = catDrinken,

        };

        var suppRibbetjes = new SupplementDto.Detail()
        {
            Id = 5,
            Name = "Ribetjes",
            Description = "1 hele varkensrib gemarineerd in pikante saus",
            Price = 10.00M,
            Category = catDrinken,
            AmountAvailable = 10,
            ImageUrls = new List<Uri> { new Uri("https://hunting.be/wp-content/uploads/2021/02/grilled-pork-ribs-P6RJQ79-scaled.jpg") }

        };

        var supBbqStel = new SupplementDto.Detail()
        {
            Id = 6,
            Category = catExtra,
            AmountAvailable = 3,
            Description = "Bbq-stel om lekker vlees op te bakken. Inclusief hout voor bbq",
            Name = "Bbq set",
            Price = 50,
            ImageUrls = new List<Uri>() { new Uri("https://mb.fqcdn.nl/square1200ng/8676170/krumble-teflon-bakmat-rond-bbq-set-van-2.jpg") },
        };

        var supBbqKit = new SupplementDto.Detail()
        {
            Id = 7,
            Category = catExtra,
            AmountAvailable = 2,
            Description = "Barbequekit 18-delig. Alles wat je nodig hebt om je bbq te gebruiken",
            Name = "Bbq-kit",
            Price = 20,
            ImageUrls = new List<Uri>() { new Uri("https://img.fruugo.com/product/4/37/575202374_max.jpg") },
            CreatedAt = DateTime.Now,
        };
        var supApertiefPlateaus = new SupplementDto.Detail()
        {
            Id = 8,
            Category = catExtra,
            AmountAvailable = 35,
            Description = "Plateaus om je aperitiefhapjes mooi te kunnen presenteren.",
            Name = "Aperitiefhapjes plateau",
            Price = 2,
            ImageUrls = new List<Uri>() { new Uri("https://www.elle.be/nl/wp-content/uploads/2021/12/nicola-dreyer-wlfiazldleu-unsplash-kopieren.jpg") },
        };


        var bierChoice = new FormulaSupplementChoiceDto.Detail()
        {
            Id = 1,
            Name = "Keuze bier",
            DefaultChoice = suppJup,
            MinQuantity = 3,
            IsQuantityNumberOfGuests = false,
            SupplementsToChoose = new List<SupplementDto.Detail>
            {
                suppJup,suppDuvel,suppStella
            }

        };

        var etenChoice = new FormulaSupplementChoiceDto.Detail()
        {
            Id = 2,
            Name = "Keuze barbequevlees",
            DefaultChoice = suppHamburger,
            MinQuantity = 0,
            IsQuantityNumberOfGuests = true,
            SupplementsToChoose = new List<SupplementDto.Detail>
            {
               suppRibbetjes,suppHamburger
            }

        };



        var formulaBasic = new FormulaDto.Detail() 
        {   
            Id = 1, 
            Title = "Basis",
            Description = "Enkel foodtruck",
            Price = 350,
            ImageUrl= new Uri("https://4.imimg.com/data4/HW/MI/MY-9647748/bronze-medal-500x500.jpg"),
            IncludedSupplements = new List<FormulaSupplementLineDto.Detail>(),
            Choices = new List<FormulaSupplementChoiceDto.Detail>()
        };

        var formulaGoTo = new FormulaDto.Detail()
        {
            Id = 2,
            Title = "Go to",
            Description = "Foodtruck met vat(en) bier inclusief glazen",
            Price = 350,
            ImageUrl= new Uri("https://png.pngtree.com/png-vector/20191212/ourmid/pngtree-second-place-silver-medal-for-sport-podium-winner-png-image_2050419.jpg"),
            IncludedSupplements = new List<FormulaSupplementLineDto.Detail>() 
            { new FormulaSupplementLineDto.Detail() 
                {
                    Id= 1,
                    Quantity = 5,
                    Supplement=supApertiefPlateaus,
                } 
            },
            Choices = new List<FormulaSupplementChoiceDto.Detail> 
            {
                   bierChoice
            },
        };

        var formulaAllIn = new FormulaDto.Detail()
        {
            Id = 3,
            Title = "All in",
            Description = "Foodtruck met vat(en) bier inclusief glazen, barbeque en barbequeset",
            Price = 500,
            ImageUrl = new Uri("https://5.imimg.com/data5/RU/UJ/MY-17868609/school-gold-medal.jpg"),
            IncludedSupplements = new List<FormulaSupplementLineDto.Detail>()
            { 
                new FormulaSupplementLineDto.Detail()
                {
                    Id= 2,
                    Quantity = 8,
                    Supplement=supApertiefPlateaus,
                },
                new FormulaSupplementLineDto.Detail()
                {
                    Id= 3,
                    Quantity = 1,
                    Supplement=supBbqStel,
                },
                new FormulaSupplementLineDto.Detail()
                {
                    Id= 4,
                    Quantity = 1,
                    Supplement=supBbqKit,
                },

            },
            Choices = new List<FormulaSupplementChoiceDto.Detail>
            {
                   bierChoice,etenChoice
            },
        };


        formulas.Add(formulaBasic);
        formulas.Add(formulaGoTo);
        formulas.Add(formulaAllIn);



    }
    public Task<FormulaResult.Index> GetAllAsync()
    {
        var result = new FormulaResult.Index()
        {
            Formulas = formulas,
            TotalAmount = formulas.Count
        };
        return Task.FromResult(result);
    }

    public Task AddFormulaSupplementChoice(int formulaId)
    {
        throw new NotImplementedException();
    }

    public Task AddFormulaSupplementLine(int formulaId)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateAsync(FormulaDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int formulaId)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(int formulaId, FormulaDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    
    public Task<FormulaDto.Detail> GetDetailAsync(int formulaId)
    {
        throw new NotImplementedException();
    }
}

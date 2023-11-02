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
       
        var supApertiefPlateaus = new SupplementDto.Detail()
        {
            Id = 8,
            Category = catExtra,
            AmountAvailable = 35,
            Description = "Plateaus om je aperitiefhapjes mooi te kunnen presenteren.",
            Name = "Aperitiefhapjes plateau",
            Price = 2,
            ImageUrls = new List<Uri>() { new Uri("https://cdn.myonlinestore.eu/9c120eab-73c2-4c08-a996-64d749bc2606/image/cache/full/bcf2e0c943c5961be00c1a0a73fa981664057eab.jpg?20230824130710") },
        };

        var supTafel = new SupplementDto.Detail()
        {
            Id = 9,
            AmountAvailable = 4,
            Category = catExtra,
            Description = "Tafel voor 8 personen.",
            Name = "Tafel",
            Price = 8,
            ImageUrls = new List<Uri>() { new Uri("https://buitenhof.xcdn.nl/images/COT-260-LAV-CRT_concept-tuintafel-260-rechthoek-lava-robuust-teak_V_1.jpg?f=rs:fit:1500:1500/g:ce&s=1") }
        };

        var supStoelen = new SupplementDto.Detail()
        {
            Id = 10,
            AmountAvailable = 40,
            Category = catExtra,
            Description = "Comfortabele stoel geschikt voor binnen en buiten.",
            Name = "Stoel",
            Price = 2,
            ImageUrls = new List<Uri>() { new Uri("https://designonline24.nl/media/wysiwyg/Hoofdcategorieen/tuin/wicker-tuinstoelen-designonline24.jpg") }
        };

        var supReceptieTafel = new SupplementDto.Detail()
        {
            Id = 11,
            AmountAvailable = 12,
            Category = catExtra,
            Description = "Ronde hoge tafel geschikt voor recepties.",
            Name = "Receptietafel",
            Price = 2,
            ImageUrls = new List<Uri>() { new Uri("https://www.fjestum.be/images/product/ReceptietafelStretchhoeszwart_116.jpg") }
        };

        var supWitteWijnglazen = new SupplementDto.Detail()
        {
            Id = 12,
            AmountAvailable = 50,
            Category = catDrinken,
            Description = "Wijnglas geschikt voor witte wijn.",
            Name = "Wijnglas witte wijn",
            Price = 0.25M,
            ImageUrls = new List<Uri>() { new Uri("https://moodsathome.com/src/Frontend/Files/Cache/products_preview_thumb/src/Frontend/Files/Products/ProductImageDetail/source/f377992f6f95152dba8adde73dae0f0ac4720f74.jpeg") }
        };

        var supRodeWijnglazen = new SupplementDto.Detail()
        {

            Id = 13,
            AmountAvailable = 50,
            Category = catDrinken,
            Description = "Wijnglas geschikt voor rode wijn.",
            Name = "Wijnglas rode wijn",
            Price = 0.25M,
            ImageUrls = new List<Uri>() { new Uri("https://media.s-bol.com/Jy9VQ6wANn8y/550x487.jpg") }
        };

        var supSfeerverlichting = new SupplementDto.Detail()
        {

            Id = 14,
            AmountAvailable = 8,
            Category = catExtra,
            Description = "Sfeerverlichting om op te hangen.",
            Name = "Sfeerverlichting",
            Price = 3,
            ImageUrls = new List<Uri>() { new Uri("https://cdn.webshopapp.com/shops/293468/files/323407380/1015x535x1/image.jpg") }
        };
        var supHapjes = new SupplementDto.Detail()
        {

            Id = 15,
            AmountAvailable = 10,
            Category = catEten,
            Description = "Vier verschillende soorten hapjes per persoon.",
            Name = "Hapjes",
            Price = 2M,
            ImageUrls = new List<Uri>() { new Uri("https://img.static-rmg.be/a/view/q75/w/h/4764567/warme-en-koude-hapjes-jpg.jpg") }
        };
        var supFruitsap = new SupplementDto.Detail()
        {

            Id = 16,
            AmountAvailable = 75,
            Category = catDrinken,
            Description = "Een bokaal vers fruitsap.",
            Name = "Fruitsap",
            Price = 5M,
            ImageUrls = new List<Uri>() { new Uri("https://weilandshof.be/wp-content/uploads/2021/04/fruitsap-orange-juice-weilandshof.jpg") }
        };
        var supLimonade = new SupplementDto.Detail()
        {
            Id = 17,
            AmountAvailable = 75,
            Category = catDrinken,
            Description = "Een bokaal zelf gemaakte limonade.",
            Name = "Limonade",
            Price = 5M,
            ImageUrls = new List<Uri>() { new Uri("https://1.bp.blogspot.com/--WOuRTUIW48/Xud2-T28wFI/AAAAAAAABAs/7svop0gvCwAM-BUve767bmx9V2OeU3D2ACNcBGAsYHQ/s1600/three-juice-jar-dispensers-3217151.jpg") }
        };

        var supChampagneWijnglazen = new SupplementDto.Detail()
        {
            Id = 18,
            AmountAvailable = 150,
            Category = catDrinken,
            Description = "Glas geschikt voor champagne.",
            Name = "Champagne glas",
            Price = 0.25M,
            ImageUrls = new List<Uri>() { new Uri("https://cdn.zilvercms.nl/x1000,q80/http://cookinglife.zilvercdn.nl/uploads/product/images/LatelierDeVin-Algemeen-3.jpg") }
        };

        
        supplements.Add(supApertiefPlateaus);
        supplements.Add(supTafel);
        supplements.Add(supStoelen);
        supplements.Add(supReceptieTafel);
        supplements.Add(supWitteWijnglazen);
        supplements.Add(supRodeWijnglazen);
        supplements.Add(supSfeerverlichting);
        supplements.Add(supHapjes);
        supplements.Add(supFruitsap);
        supplements.Add(supLimonade);
        supplements.Add(supChampagneWijnglazen);
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

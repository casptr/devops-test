using System.Text.Json;
namespace Packages
{


    public class PackageDataGenerator
    {
        private readonly HttpClient _httpClient;
        public List<PackageSupplement> BeverageList { get; set; }
        public List<PackageSupplement> FoodList { get; set; }
        public List<PackageSupplement> KitchenList { get; set; }
        public List<PackageSupplement> GlasswareList { get; set; }
        public List<PackageSupplement> TablewareList { get; set; }
        public List<PackageSupplement> FurnitureList { get; set; }
        public List<PackageSupplement> LightingList { get; set; }
        public List<PackageSupplement> HeatingList { get; set; }

        public PackageSupplements SupplementsList { get; set; }

        public PackageDataGenerator(HttpClient httpClient)
        {
            _httpClient = httpClient;
            SupplementsList = new PackageSupplements
            {
                SupplementList = new List<List<PackageSupplement>>()
            };
        }
        public async Task InitializeDataAsync()
        {
            var BeverageListJson = await _httpClient.GetStringAsync("/json_data/dranken.json");
            BeverageList = JsonSerializer.Deserialize<List<PackageSupplement>>(BeverageListJson);

            var FoodListJson = await _httpClient.GetStringAsync("/json_data/eten.json");
            FoodList = JsonSerializer.Deserialize<List<PackageSupplement>>(FoodListJson);

            var KitchenListJson = await _httpClient.GetStringAsync("/json_data/keuken.json");
            KitchenList = JsonSerializer.Deserialize<List<PackageSupplement>>(KitchenListJson);

            var GlasswareListJson = await _httpClient.GetStringAsync("/json_data/glazen.json");
            GlasswareList = JsonSerializer.Deserialize<List<PackageSupplement>>(GlasswareListJson);

            var TablewareListJson = await _httpClient.GetStringAsync("/json_data/servies.json");
            TablewareList = JsonSerializer.Deserialize<List<PackageSupplement>>(TablewareListJson);

            var FurnitureListJson = await _httpClient.GetStringAsync("/json_data/meubilair.json");
            FurnitureList = JsonSerializer.Deserialize<List<PackageSupplement>>(FurnitureListJson);

            var LightingListJson = await _httpClient.GetStringAsync("/json_data/verlichting.json");
            LightingList = JsonSerializer.Deserialize<List<PackageSupplement>>(LightingListJson);

            var HeatingListJson = await _httpClient.GetStringAsync("/json_data/verwarming.json");
            HeatingList = JsonSerializer.Deserialize<List<PackageSupplement>>(HeatingListJson);

            // SupplementsList = new PackageSupplements();
            SupplementsList.SupplementList.Add(BeverageList);
            SupplementsList.SupplementList.Add(FoodList);
            SupplementsList.SupplementList.Add(KitchenList);
            SupplementsList.SupplementList.Add(GlasswareList);
            SupplementsList.SupplementList.Add(TablewareList);
            SupplementsList.SupplementList.Add(FurnitureList);
            SupplementsList.SupplementList.Add(LightingList);
            SupplementsList.SupplementList.Add(HeatingList);
        }

    }

}
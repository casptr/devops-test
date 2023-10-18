using System.Collections;
using Ardalis.GuardClauses;

namespace Packages
{
    public class Package
    {
        public PackageFormula? Formula { get; set; }
        public PackageSupplements? Supplements { get; set; }
    }

    public class PackageFormula
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PackageProducts Products { get; set; }

        public PackageFormula(string id, string name, decimal price, PackageProducts products)
        {
            Guard.Against.NullOrWhiteSpace(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NegativeOrZero(price, nameof(price));
            Guard.Against.Null(products, nameof(products));

            Id = id;
            Name = name;
            Price = price;
            Products = products;
        }
    }

    public class PackageProducts
    {
        public List<PackageProduct>? ProductList { get; set; }
    }
    public class PackageProduct
    {
        public string? Id { get; set; }
        public string? Category { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Imagery { get; set; }
        public int? Quantity { get; set; }
        public int? AmountAvailable { get; set; }
        public bool? IsSupplement { get; set; }


        public PackageProduct()
        {
            IsSupplement = false;
        }
    }
    public class PackageSupplement
    {
        public string? Id { get; set; }
        public string? Category { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Imagery { get; set; }
        public int? AmountAvailable { get; set; }
        public bool? IsSupplement { get; set; }
    }
    public class PackageSupplements : IEnumerable<List<PackageSupplement>>
    {
        public List<List<PackageSupplement>>? SupplementList { get; set; }
        public IEnumerator<List<PackageSupplement>> GetEnumerator()
        {
            if (SupplementList != null)
            {
                return SupplementList.GetEnumerator();
            }
            return new List<List<PackageSupplement>>().GetEnumerator();

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
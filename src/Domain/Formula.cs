using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Formula
{
  
    private readonly List<Supplement> includedSupplemets= new();
    public IReadOnlyCollection<Supplement> IncludedSupplements => includedSupplemets.AsReadOnly();

    private string name = default!;
    public string Name 
    { 
        get=>name;
        set => name = Guard.Against.NullOrWhiteSpace(value, nameof(Name));
    }
    private string description = default!;
    public string Description 
    {
        get=>description; 
        set=> description = Guard.Against.NullOrWhiteSpace(value, nameof(Description)); 
    }
   
    private double price = default!;
    public double Price
    {
        get => price;
        set => price = Guard.Against.Negative(value, nameof(Price));
    }

    private string imageUrl = default!;
    public string ImageUrl
    {
        get => imageUrl;
        set => imageUrl = Guard.Against.NullOrWhiteSpace(value, nameof(ImageUrl));
    }

    public Formula(){ }
    public Formula(string name, string description, double price, string imageUrl) 
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }
}

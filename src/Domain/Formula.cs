using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Formula : Entity
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
   
    private Money price = default!;
    public Money Price
    {
        get => price;
        set => price = Guard.Against.Null(value, nameof(Price));
    }

    private string imageUrl = default!;
    public string ImageUrl
    {
        get => imageUrl;
        set => imageUrl = Guard.Against.NullOrWhiteSpace(value, nameof(ImageUrl));
    }

    private Formula(){ }
    public Formula(string name, string description, Money price, string imageUrl) 
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }
}

using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Supplement
{
    private string name = default!;
    public string Name
    {
        get => name;
        set => name = Guard.Against.NullOrWhiteSpace(value, nameof(Name));
    }

    private string description = default!;
    public string Description
    {
        get => description;
        set => description = Guard.Against.NullOrWhiteSpace(value, nameof(Description));
    }

    private string category = default!;
    public string Category
    {
        get => category;
        set => category = Guard.Against.NullOrWhiteSpace(value,nameof(Category));
    }

    private double price= default!;
    public double Price 
    { 
        get=>price; 
        set=>price = Guard.Against.Negative(value, nameof(Price));
    }

    private string imageUrl = default!;
    public string ImageUrl
    {
        get => imageUrl;
        set => imageUrl = Guard.Against.NullOrWhiteSpace(value, nameof(ImageUrl));
    }

    private int amountAvailable=default!;
    public int AmountAvailable 
    { get=>amountAvailable; 
      set=>amountAvailable = Guard.Against.Negative(value,nameof(AmountAvailable)); 
    }

    public Supplement(string name, string description, string category, double price, string imageUrl, int amountAvailable) 
    { 
        Name = name;
        Description = description;
        Category = category;
        Price = price;
        ImageUrl = imageUrl;
        AmountAvailable = amountAvailable;
       
    }

}

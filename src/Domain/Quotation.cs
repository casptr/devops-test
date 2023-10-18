using Domain;

namespace Quotation
{
    public class QuotationModel
    {
        public Owner Owner { get; set; }
        public Customer Customer { get; set; }
        public Company Company {get; set;}
        public Formula Formula {get; set; }
        public Supplements Supplements { get; set; }
        public Reservation Reservation { get; set; }
    }

    public class Owner
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }

    public class Customer
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }

    public class Company
    {
        public string Name {get; set;}
        public string VAT {get; set;}
        public Address Address {get; set;}
        public ContactInfo ContactInfo {get; set;}

    }

    public class Package
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Products Products { get; set; }

    }

    public class Products
    {
        public List<Product> ProductList { get; set; }
    }
public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsSupplement { get; set; }

    public Product()
    {
        IsSupplement = false;
    }
}

public class Supplement : Product
{
    public Supplement()
    {
        IsSupplement = true;
    }
}

    public class Supplements
    {
        public List<Supplement> SupplementList { get; set; }
    }

    public class Reservation
    {
        public string Id { get; set; }
        public Address Address { get; set; }
        public DateTime Datetime_start { get; set; }
        public DateTime Datetime_end { get; set; }

    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

    }

    public class ContactInfo
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
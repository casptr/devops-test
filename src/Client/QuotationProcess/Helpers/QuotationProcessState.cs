using Domain.Customers;
using Foodtruck.Shared.Customers;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Quotations;
using Foodtruck.Shared.Supplements;

namespace Foodtruck.Client.QuotationProcess.Helpers
{
    public class QuotationProcessState
    {
        private QuotationDto.Create configuringQuotation = new();
        public QuotationVersionDto.Create ConfiguringQuotationVersion => configuringQuotation.QuotationVersion;
        public CustomerDto.Create Customer => configuringQuotation.Customer;
        public AddressDto EventAddress => configuringQuotation.QuotationVersion.EventAddress;
        public AddressDto BillingAddress => configuringQuotation.QuotationVersion.BillingAddress;

        private readonly List<FormulaChoice> formulaChoices = new();
        public IReadOnlyCollection<FormulaChoice> FormulaChoices => formulaChoices.AsReadOnly();

        private readonly List<SupplementChoice> supplementChoices = new();
        public IReadOnlyCollection<SupplementChoice> SupplementChoices => supplementChoices.AsReadOnly();

        // Item is chosen if: quantity is dependent on number of guests => checkbox true, quantity dependent on input => quantity != 0
        public IReadOnlyCollection<FormulaChoiceItem> ChosenFormulaChoiceItems => FormulaChoices.SelectMany(choice => choice.Options.Where(option => choice.IsQuantityNumberOfGuests ? option.IsChosen : option.Quantity != 0)).ToList();

        public FormulaDto.Detail? CurrentSelectedFormula { get; set; }

        public void ConfigureReservation(DateTime start, DateTime end)
        {
            ConfiguringQuotationVersion.Reservation.Start = start;
            ConfiguringQuotationVersion.Reservation.End = end;
            Console.WriteLine($"Reservation from {start} to {end}");
        }

        public bool HasConfiguredFormulaChoices(FormulaDto.Detail formula)
        {
            return formulaChoices.Count != 0 && CurrentSelectedFormula == formula;
        }

        public void ConfigureFormula(FormulaDto.Detail formula, List<FormulaChoice>? formulaChoices = null)
        {
            CurrentSelectedFormula = formula;

            this.formulaChoices.Clear();
            if (formulaChoices is not null)
            {
                this.formulaChoices.AddRange(formulaChoices);

            }

        }

        public void AddSupplement(SupplementChoice supplement)
        {
            // TODO: Add check to see if quantity that is gonna be added doesn't go over the AmountAvailable otherwise user can add more than available amount of a certain item
            if (supplementChoices.Contains(supplement))
            {
                supplementChoices.Where(supplementChoice => supplementChoice.Equals(supplement)).ToList().ForEach(supplementChoice => supplementChoice.Quantity += supplement.Quantity);
            }
            else
            {
                supplementChoices.Add(supplement);
            }
            Console.WriteLine();
            foreach (var choice in supplementChoices)
            {
                Console.WriteLine(choice.Supplement.Name + ": " + choice.Quantity.ToString());
            }
        }

        // Finish quotation request
        // add all formula included supplement items 
        // add all choices
        public void RequestQuotation()
        {
            // Add choices
            foreach (FormulaChoice formulaChoice in formulaChoices)
            {
                if (formulaChoice.IsQuantityNumberOfGuests)
                {
                    ConfiguringQuotationVersion.Items.AddRange(formulaChoice.Options.Where(option => option.IsChosen).Select(option => new SupplementItemDto.Create()
                    {
                        SupplementId = option.Supplement.Id,
                        Quantity = 50  // TODO Quantity of SupplementItemDto should come from the number of guests here
                    }));
                }
                else
                {
                    ConfiguringQuotationVersion.Items.AddRange(formulaChoice.Options.Where(option => option.Quantity != 0).Select(option => new SupplementItemDto.Create()
                    {
                        SupplementId = option.Supplement.Id,
                        Quantity = option.Quantity
                    }));
                }
            }

            // Add included supplements
            ConfiguringQuotationVersion.Items.AddRange(CurrentSelectedFormula.IncludedSupplements.Select(includedSupplementLine => new SupplementItemDto.Create()
            {
                SupplementId = includedSupplementLine.Supplement.Id,
                Quantity = includedSupplementLine.Quantity
            }));

            // TEMP
            PrintQuotation();
        }

        public void PrintQuotation()
        {
            Console.WriteLine("------------QUOTATION----------------");
            Console.WriteLine($"Reservation from {ConfiguringQuotationVersion.Reservation.Start} to {ConfiguringQuotationVersion.Reservation.End}");
            Console.WriteLine();

            Console.WriteLine("Customer Details:");
            Console.WriteLine($"Firstname: {Customer.Firstname}");
            Console.WriteLine($"Lastname: {Customer.LastName}");
            Console.WriteLine($"Email: {Customer.Email}");
            Console.WriteLine($"Phone: {Customer.Phone}");
            Console.WriteLine($"CompanyName: {Customer.CompanyName}");
            Console.WriteLine($"CompanyNumber: {Customer.CompanyNumber}");
            Console.WriteLine($"WantsMarketingMails: {Customer.WantsMarketingMails}");

            Console.WriteLine();
            Console.WriteLine("Event adress:");
            Console.WriteLine($"Street: {EventAddress.Street}");
            Console.WriteLine($"Housenumber: {EventAddress.HouseNumber}");
            Console.WriteLine($"City: {EventAddress.City}");
            Console.WriteLine($"Zip: {EventAddress.Zip}");

            Console.WriteLine();
            Console.WriteLine("Billing adress:");
            Console.WriteLine($"Street: {BillingAddress.Street}");
            Console.WriteLine($"Housenumber: {BillingAddress.HouseNumber}");
            Console.WriteLine($"City: {BillingAddress.City}");
            Console.WriteLine($"Zip: {BillingAddress.Zip}");

            // Temp to have supplements name in writeline here
            List<SupplementDto.Detail> allSupplements = new List<SupplementDto.Detail>();
            allSupplements.AddRange(formulaChoices.SelectMany(choice => choice.Options.Select(option => option.Supplement)).ToHashSet());
            allSupplements.AddRange(CurrentSelectedFormula.IncludedSupplements.Select(i => i.Supplement));

            Console.WriteLine();
            Console.WriteLine("Supplements included and chosen:");
            foreach (var supplementItem in ConfiguringQuotationVersion.Items)
            {
                var supplement = allSupplements.Find(s => s.Id == supplementItem.SupplementId);
                Console.WriteLine($"Supplement: {supplement.Name}, Quantity: {supplementItem.Quantity}");
            }

            Console.WriteLine("---------------------------------------");


        }



    }

}
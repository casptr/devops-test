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

        // Item is chosen if: quantity is dependent on number of guests => checkbox true, quantity dependent on input => quantity != 0
        public IReadOnlyCollection<FormulaChoiceItem> ChosenFormulaChoiceItems => FormulaChoices.SelectMany(choice => choice.Options.Where(option => option.Quantity != 0)).ToList();

        public FormulaDto.Detail? CurrentSelectedFormula { get; set; }

        public CustomerDetailsFormModel CustomerDetailsFormModel { get; set; } = new();

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

        public void ConfigureQuotationCustomerDetails()
        {
            configuringQuotation.Customer = CustomerDetailsFormModel.Customer;
            configuringQuotation.QuotationVersion.EventAddress = CustomerDetailsFormModel.EventAddress;
            configuringQuotation.QuotationVersion.ExtraInfo = CustomerDetailsFormModel.ExtraInfo;

            // we have to set the billing address if event address is the same as the billing address
            if (CustomerDetailsFormModel.IsEventAddressDifferentThanBillingAddress)
            {
                configuringQuotation.QuotationVersion.BillingAddress = CustomerDetailsFormModel.BillingAddress;
            }
            else
            {
                configuringQuotation.QuotationVersion.BillingAddress.Street = CustomerDetailsFormModel.EventAddress.Street;
                configuringQuotation.QuotationVersion.BillingAddress.HouseNumber = CustomerDetailsFormModel.EventAddress.HouseNumber;
                configuringQuotation.QuotationVersion.BillingAddress.City = CustomerDetailsFormModel.EventAddress.City;
                configuringQuotation.QuotationVersion.BillingAddress.Zip = CustomerDetailsFormModel.EventAddress.Zip;
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
                ConfiguringQuotationVersion.Items.AddRange(formulaChoice.Options.Where(option => option.Quantity != 0).Select(option => new SupplementItemDto.Create()
                {
                    SupplementId = option.Supplement.Id,
                    Quantity = option.Quantity
                }));
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
            Console.WriteLine($"Lastname: {Customer.Lastname}");
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
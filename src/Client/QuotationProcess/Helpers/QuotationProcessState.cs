using Domain.Customers;
using Domain.Formulas;
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


        public FormulaDto.Detail? CurrentSelectedFormula { get; private set; }

        private List<FormulaChoiceFormModel> CompletedChoiceFormModels = new();
        private readonly List<FormulaChoiceFormModel> choiceFormModels = new();
        public IReadOnlyList<FormulaChoiceFormModel> ChoiceFormModels => choiceFormModels.AsReadOnly();

        // Item is chosen if quantity != 0
        public IReadOnlyCollection<FormulaChoiceFormItem> ChosenFormulaChoiceItems => CompletedChoiceFormModels.SelectMany(choice => choice.Options.Where(option => option.Quantity != 0)).ToList();


        public CustomerDetailsFormModel CustomerDetailsFormModel { get; set; } = new();

        public void ConfigureReservation(DateTime start, DateTime end)
        {
            ConfiguringQuotationVersion.Reservation.Start = start;
            ConfiguringQuotationVersion.Reservation.End = end;
            Console.WriteLine($"Reservation from {start} to {end}");
        }

        public void SetupFormulaChoiceFormModels(FormulaDto.Detail formula)
        {
            choiceFormModels.Clear();

            if (formula.Choices?.Count() == 0)
                return;

            choiceFormModels.AddRange(formula.Choices.Select(choice => new FormulaChoiceFormModel(choice)));

            // Apply the previously chosen quantities
            if(CurrentSelectedFormula == formula)
            {
                for(int choiceIndex = 0; choiceIndex < choiceFormModels.Count; choiceIndex++)
                {
                    for(int optionIndex = 0; optionIndex < choiceFormModels[choiceIndex].Options.Count(); optionIndex++)
                    {
                        choiceFormModels[choiceIndex].Options[optionIndex].Quantity = CompletedChoiceFormModels[choiceIndex].Options[optionIndex].Quantity;
                    }
                }
            }
        }

        public void ConfigureQuotationFormula(FormulaDto.Detail formula)
        {
            Console.WriteLine("ConfigureQuotationFormula");
            CurrentSelectedFormula = formula;
            CompletedChoiceFormModels.Clear();
            CompletedChoiceFormModels.AddRange(ChoiceFormModels.ToList());

            ConfiguringQuotationVersion.FormulaSupplementItems.Clear();
            configuringQuotation.QuotationVersion.FormulaId = CurrentSelectedFormula.Id;

            // Add formula choices that where chosen
            foreach (FormulaChoiceFormModel formulaChoice in CompletedChoiceFormModels)
            {
                ConfiguringQuotationVersion.FormulaSupplementItems.AddRange(formulaChoice.Options.Where(option => option.Quantity != 0).Select(option => new SupplementItemDto.Create()
                {
                    SupplementId = option.Supplement.Id,
                    Quantity = option.Quantity
                }));
            }

            // Add included supplements from formula
            ConfiguringQuotationVersion.FormulaSupplementItems.AddRange(CurrentSelectedFormula.IncludedSupplements.Select(includedSupplementLine => new SupplementItemDto.Create()
            {
                SupplementId = includedSupplementLine.Supplement.Id,
                Quantity = includedSupplementLine.Quantity
            }));

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
                configuringQuotation.QuotationVersion.BillingAddress = CustomerDetailsFormModel.EventAddress;
            }
        }

        // Finish quotation request
        // add all formula included supplement items 
        // add all choices
        public void RequestQuotation()
        {
            // Add choices
            //foreach (FormulaChoiceFormModel formulaChoice in formulaChoices)
            //{
            //    ConfiguringQuotationVersion.Items.AddRange(formulaChoice.Options.Where(option => option.Quantity != 0).Select(option => new SupplementItemDto.Create()
            //    {
            //        SupplementId = option.Supplement.Id,
            //        Quantity = option.Quantity
            //    }));
            //}

            //// Add included supplements
            //ConfiguringQuotationVersion.Items.AddRange(CurrentSelectedFormula.IncludedSupplements.Select(includedSupplementLine => new SupplementItemDto.Create()
            //{
            //    SupplementId = includedSupplementLine.Supplement.Id,
            //    Quantity = includedSupplementLine.Quantity
            //}));

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
            allSupplements.AddRange(choiceFormModels.SelectMany(choice => choice.Options.Select(option => option.Supplement)).ToHashSet());
            allSupplements.AddRange(CurrentSelectedFormula.IncludedSupplements.Select(i => i.Supplement));

            Console.WriteLine();
            Console.WriteLine("Supplements included and chosen:");
            foreach (var supplementItem in ConfiguringQuotationVersion.FormulaSupplementItems)
            {
                var supplement = allSupplements.Find(s => s.Id == supplementItem.SupplementId);
                Console.WriteLine($"Formula Supplement: {supplement.Name}, Quantity: {supplementItem.Quantity}");
            }

            Console.WriteLine("---------------------------------------");


        }



    }

}
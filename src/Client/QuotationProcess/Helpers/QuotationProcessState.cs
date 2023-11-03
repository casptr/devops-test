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

        public FormulaDto.Detail? CurrentSelectedFormula { get; private set; }

        private readonly List<FormulaChoiceFormModel> CompletedChoiceFormModels = new();
        private readonly List<FormulaChoiceFormModel> choiceFormModels = new();
        public IReadOnlyList<FormulaChoiceFormModel> ChoiceFormModels => choiceFormModels.AsReadOnly();

        // Item is chosen if quantity != 0
        public IReadOnlyCollection<FormulaChoiceFormItem> ChosenFormulaChoiceItems => CompletedChoiceFormModels.SelectMany(choice => choice.Options.Where(option => option.Quantity != 0)).ToList();



        public CustomerDetailsFormModel CustomerDetailsFormModel { get; set; } = new();

        private readonly List<ExtraSupplement> supplementChoices = new();
        public IReadOnlyCollection<ExtraSupplement> SupplementChoices => supplementChoices.AsReadOnly();

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
            if (CurrentSelectedFormula?.Id == formula.Id)
            {
                for (int choiceIndex = 0; choiceIndex < choiceFormModels.Count; choiceIndex++)
                {
                    for (int optionIndex = 0; optionIndex < choiceFormModels[choiceIndex].Options.Count(); optionIndex++)
                    {
                        choiceFormModels[choiceIndex].Options[optionIndex].Quantity = CompletedChoiceFormModels[choiceIndex].Options[optionIndex].Quantity;
                    }
                }
            }
        }

        public void ConfigureQuotationFormula(FormulaDto.Detail formula)
        {
            CompletedChoiceFormModels.Clear();
            CompletedChoiceFormModels.AddRange(ChoiceFormModels.ToList());
            ConfiguringQuotationVersion.FormulaSupplementItems.Clear();

            CurrentSelectedFormula = formula;
            configuringQuotation.QuotationVersion.FormulaId = CurrentSelectedFormula.Id;

            ConfiguringQuotationVersion.FormulaSupplementItems.AddRange(CompletedChoiceFormModels
                .SelectMany(choiceForm => choiceForm.Options)
                .Where(option => option.Quantity != 0)
                .Select(option => new SupplementItemDto.Create()
                {
                    SupplementId = option.Supplement.Id,
                    Quantity = option.Quantity
                }));


            // Add included supplements from formula
            ConfiguringQuotationVersion.FormulaSupplementItems.AddRange(CurrentSelectedFormula.IncludedSupplements
                .Select(includedSupplementLine => new SupplementItemDto.Create()
                {
                    SupplementId = includedSupplementLine.Supplement.Id,
                    Quantity = includedSupplementLine.Quantity
                }));
        }

        public int CalculateMaxAmount(ExtraSupplement supplement)
        {
            ExtraSupplement? supplementChosen = supplementChoices.Where(supplementChoice => supplementChoice.Equals(supplement)).FirstOrDefault();

            if (supplementChosen is null)
            {
                return supplement.Supplement.AmountAvailable;
            }

            int currentAmount = supplementChosen.Quantity;
            return supplementChosen.Supplement.AmountAvailable - currentAmount;
        }

        public void AddSupplement(ExtraSupplement supplement)
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
        public void RequestQuotation()
        {
            // TEMP
            PrintQuotation();
        }

        public void PrintQuotation()
        {
            CustomerDto.Create Customer = configuringQuotation.Customer;
            AddressDto EventAddress = configuringQuotation.QuotationVersion.EventAddress;
            AddressDto BillingAddress = configuringQuotation.QuotationVersion.BillingAddress;

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
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Quotations;
using Foodtruck.Shared.Supplements;

namespace Foodtruck.Client.QuotationProcess.Helpers
{
    public class QuotationProcessState
    {
        private QuotationDto.Create configuringQuotation = new();
        public QuotationVersionDto.Create ConfiguringQuotationVersion => configuringQuotation.QuotationVersion;

        private readonly List<FormulaChoice> formulaChoices = new();
        public IReadOnlyCollection<FormulaChoice> FormulaChoices => formulaChoices.AsReadOnly();

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

        // What have i created
        public List<FormulaChoiceItem> GetChosenFormulaChoiceItems()
        {
            List<FormulaChoiceItem> chosenFormulaChoiceItems = new();

            foreach (FormulaChoice formulaChoice in formulaChoices)
            {
                if (formulaChoice.IsQuantityNumberOfGuests)
                {

                    chosenFormulaChoiceItems.AddRange(formulaChoice.Options.Where(option => option.IsChosen));
                }
                else
                {
                    chosenFormulaChoiceItems.AddRange(formulaChoice.Options.Where(option => option.Quantity != 0));
                }
            }
            return chosenFormulaChoiceItems;
        }

        //public void ResetFormula()
        //{
        //    CurrentSelectedFormula = null;
        //    formulaChoices.Clear();
        //}



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





        }



    }

}
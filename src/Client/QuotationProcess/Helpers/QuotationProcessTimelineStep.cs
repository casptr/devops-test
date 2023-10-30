namespace Foodtruck.Client.QuotationProcess.Helpers
{
    public class QuotationProcessTimelineStep
    {
        public QuotationProcessStep Step { get; set; }
        public string StepTitle { get; set; }

        public QuotationProcessTimelineStep(QuotationProcessStep step, string stepTitle)
        {
            Step = step;
            StepTitle = stepTitle;
        }
    }

    public enum QuotationProcessStep
    {
        CHOOSE_DATE,
        SELECT_FORMULA,
        ADD_SUPPLEMENTS,
        ENTER_DETAILS,
    }
}

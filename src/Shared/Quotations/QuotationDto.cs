using System.Data;
using Domain.Quotations;
using FluentValidation;

namespace Foodtruck.Shared.Quotations;

public abstract class QuotationDto{


    // offerte aanvraag als 
    public class RequestForQuotation
    {
        public QuotationVersion QuotationVersion {get; set;} = default!;

    }

    public class Mutate
    {
        public QuotationVersion? QuotationVersion {get; set;}

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator(){
                RuleFor(q => q.QuotationVersion).NotNull();
            }
        }
    }
}
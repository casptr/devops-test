using System.Data;
using Domain.Quotations;
using FluentValidation;
using Foodtruck.Shared.Customers;

namespace Foodtruck.Shared.Quotations;

public abstract class QuotationDto
{

    public class Index
    {
        public int Id { get; set; }
        public CustomerDto.Detail? Customer { get; set; }
        public QuotationVersionDto.Index? LastQuotationVersion { get; set; }
    }

    public class Detail
    {
        public int Id { get; set; }
        public CustomerDto.Detail? Customer { get; set; }
        public IEnumerable<QuotationVersionDto.Detail>? QuotationVersions { get; set; }
    }

    public class Create
    {
        public CustomerDto.Create Customer { get; set; } = new();
        public QuotationVersionDto.Create QuotationVersion { get; set; } = new();

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {
                RuleFor(q => q.QuotationVersion).NotNull();
            }
        }
    }
}
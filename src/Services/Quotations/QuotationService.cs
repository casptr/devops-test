using Domain.Common;
using Domain.Customers;
using Domain.Exceptions;
using Domain.Formulas;
using Domain.Quotations;
using Foodtruck.Persistence;
using Foodtruck.Shared.Customers;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Quotations;
using Foodtruck.Shared.Reservations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Quotations
{
    public class QuotationService : IQuotationService
    {
        private readonly FoodtruckDbContext dbContext;

        public QuotationService(FoodtruckDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateAsync(QuotationDto.Create model)
        {
            throw new NotImplementedException();
        }




        public async Task<QuotationDto.Detail> GetDetailAsync(int quotationId)
        {
            QuotationDto.Detail? quotation = await dbContext.Quotations.Select(x => new QuotationDto.Detail()
            {
                Id = x.Id,
                Customer = new CustomerDto.Detail()
                {
                    Firstname = x.Customer.Firstname,
                    Lastname = x.Customer.Lastname,
                    Email = x.Customer.Email.Value,
                    Phone = x.Customer.Phone,
                    CompanyName = x.Customer.CompanyName,
                    CompanyNumber = x.Customer.CompanyNumber,
                },
                QuotationVersions = x.Versions.Select(version => new QuotationVersionDto.Detail()
                {
                    Id = version.Id,
                    VersionNumber = version.VersionNumber,
                    NumberOfGuests = version.NumberOfGuests,
                    ExtraInfo = version.ExtraInfo,
                    Description = version.Description,
                    Reservation = new ReservationDto.Detail()
                    {
                        Id = version.Reservation.Id,
                        Description = version.Reservation.Description,
                        Start = version.Reservation.Start,
                        End = version.Reservation.End,
                    },
                    Formula = new FormulaDto.Index()
                    {
                        Id = version.Formula.Id,
                        Title = version.Formula.Title,
                    },
                    EventAddress = new AddressDto()
                    {
                        Street = version.EventAddress.Street,
                        City = version.EventAddress.City,
                        HouseNumber = version.EventAddress.HouseNumber,
                        Zip = version.EventAddress.Zip
                    },
                    BillingAddress = new AddressDto()
                    {
                        Street = version.BillingAddress.Street,
                        City = version.BillingAddress.City,
                        Zip = version.BillingAddress.Zip,
                        HouseNumber = version.BillingAddress.HouseNumber,
                    },
                    FoodtruckPrice = version.FoodtruckPrice.Value,
                    Price = version.Price.Value,
                    VatTotal = version.VatTotal.Value,

                    FormulaSupplementLines = version.QuotationSupplementLines.Where(quotationSupplementLine => quotationSupplementLine.IsIncludedInFormula).Select(quotationSupplementLine => new QuotationSupplementLineDto()
                    {
                        Id = quotationSupplementLine.Id,
                        Description = quotationSupplementLine.Description,
                        Name = quotationSupplementLine.Name,
                        Quantity = quotationSupplementLine.Quantity,
                        SupplementPrice = quotationSupplementLine.SupplementPrice.Value,
                        SupplementVat = quotationSupplementLine.SupplementVat.Value,
                    }),

                    ExtraSupplementLines = version.QuotationSupplementLines.Where(quotationSupplementLine => !quotationSupplementLine.IsIncludedInFormula).Select(quotationSupplementLine => new QuotationSupplementLineDto()
                    {
                        Id = quotationSupplementLine.Id,
                        Description = quotationSupplementLine.Description,
                        Name = quotationSupplementLine.Name,
                        Quantity = quotationSupplementLine.Quantity,
                        SupplementPrice = quotationSupplementLine.SupplementPrice.Value,
                        SupplementVat = quotationSupplementLine.SupplementVat.Value,
                    }),

                })
            }).SingleOrDefaultAsync(x => x.Id == quotationId);

            if (quotation is null)
                throw new EntityNotFoundException(nameof(Quotation), quotationId);

            return quotation;
        }
    }
}

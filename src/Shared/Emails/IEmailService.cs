using Foodtruck.Shared.Quotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Emails;

public interface IEmailService
{
    Task<bool> SendNewQuotationConfirmationToCustomer(QuotationDto.Detail quotation);
    Task<bool> SendNewQuotationPdfToAdmin(QuotationDto.Detail quotation);
}

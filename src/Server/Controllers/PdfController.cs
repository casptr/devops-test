using Foodtruck.Shared.Emails;
using Foodtruck.Shared.Pdfs;
using Foodtruck.Shared.Quotations;
using Microsoft.AspNetCore.Mvc;
using Services.Emails;
using Services.Pdfs;

namespace Foodtruck.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PdfController : Controller
{
    private readonly IQuotationService quotationService;
    private readonly IPdfService pdfService;
    public PdfController(IQuotationService quotationService, IEmailService emailService, IPdfService pdfService)
    {
        this.quotationService = quotationService;
        this.pdfService = pdfService;
    }
    public async Task<string> GetQuotationPdf(int quotationId)
    {
        QuotationDto.Detail quotation = await quotationService.GetDetailAsync(quotationId);
        QuotationVersionDto.Detail quotationVersion;
        string base64;
        if (quotation != null && quotation.QuotationVersions != null)
        {
            quotationVersion = quotation.QuotationVersions.Last();
            base64 = await pdfService.GetQuotationPdfAsBase64(quotation, quotationVersion, "pdftext");
            return base64;
        }
        return "";
    }
}


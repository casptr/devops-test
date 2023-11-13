using Foodtruck.Shared.Emails;
using Foodtruck.Shared.Pdfs;
using Foodtruck.Shared.Quotations;
using Microsoft.AspNetCore.Mvc;
using Services.Emails;
using Services.Pdfs;
using Swashbuckle.AspNetCore.Annotations;

namespace Foodtruck.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : Controller
{
    private readonly IQuotationService quotationService;
    private readonly IEmailService emailService;
    private readonly IPdfService pdfService;
    public EmailController(IQuotationService quotationService, IEmailService emailService, IPdfService pdfService)
    {
        this.quotationService = quotationService;
        this.emailService = emailService;
        this.pdfService = pdfService;
    }

    [SwaggerOperation("Sends a quotation pdf via mail")]
    [HttpGet("mail/{quotationId}")]
    public async Task<bool> SendQuotationPdfEmail(int quotationId)
    {
        QuotationDto.Detail quotation = await quotationService.GetDetailAsync(quotationId);
        QuotationVersionDto.Detail quotationVersion;
        string base64;
        if (quotation != null && quotation.QuotationVersions != null)
        {
            quotationVersion = quotation.QuotationVersions.Last();
            base64 = await pdfService.GetQuotationPdfAsBase64(quotation, quotationVersion, "Woef zegt de kat.");
            await emailService.SendQuotationPdfEmail(base64, "Woef zegt de kat.");
            Console.WriteLine(base64);
        }
        return true;
    }
}


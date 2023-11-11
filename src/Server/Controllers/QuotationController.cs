using Foodtruck.Shared.Customers;
using Foodtruck.Shared.Emails;
using Foodtruck.Shared.Pdfs;
using Foodtruck.Shared.Quotations;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Pdfs;
using Swashbuckle.AspNetCore.Annotations;

namespace Foodtruck.Server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class QuotationController : Controller
{
    private readonly IQuotationService quotationService;
    private readonly IEmailService emailService;
    private readonly IPdfService pdfService;

    public QuotationController(IQuotationService quotationService, IEmailService emailService, IPdfService pdfService)
    {
        this.quotationService = quotationService;
        this.emailService = emailService;
        this.pdfService = pdfService;
    }

    [SwaggerOperation("Returns a list of all the quotations.")]
    [HttpGet]
    public async Task<QuotationResult.Index> GetIndex([FromQuery] QuotationRequest.Index request)
    {
        await emailService.SendEmail("halloooooooooooo");
        return await quotationService.GetIndexAsync(request);

    }


    [SwaggerOperation("Create a new quotation")]
    [HttpPost]  // TODO: Roles - Authorize(Roles = Roles.Administrator) 
    public async Task<IActionResult> Create(QuotationDto.Create model)
    {
        int quotationId = await quotationService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), quotationId) ;
    }

    [SwaggerOperation("Returns a specific quotation.")]
    [HttpGet("{quotationId}")]
    public async Task<QuotationDto.Detail> GetDetail(int quotationId)
    {
        return await quotationService.GetDetailAsync(quotationId);
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
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
        return await quotationService.GetIndexAsync(request);
    }


    [SwaggerOperation("Create a new quotation")]
    [HttpPost]  // TODO: Roles - Authorize(Roles = Roles.Administrator) 
    public async Task<IActionResult> Create(QuotationDto.Create model)
    {
        int quotationId = await quotationService.CreateAsync(model);

        QuotationDto.Detail quotation = await GetDetail(quotationId);
        await emailService.SendNewQuotationPdfToAdmin(quotation);
        await emailService.SendNewQuotationConfirmationToCustomer(quotation);

        return CreatedAtAction(nameof(Create), quotationId);
    }

    [SwaggerOperation("Returns a specific quotation.")]
    [HttpGet("{quotationId}")]
    public async Task<QuotationDto.Detail> GetDetail(int quotationId)
    {
        return await quotationService.GetDetailAsync(quotationId);
    }


}
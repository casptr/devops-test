using Foodtruck.Shared.Customers;
using Foodtruck.Shared.Quotations;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Foodtruck.Server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class QuotationController : Controller
{
    private readonly IQuotationService quotationService;

    public QuotationController(IQuotationService quotationService)
    {
        this.quotationService = quotationService;
    }

    // [SwaggerOperation("Submit a request for a quotation")]
    // [HttpPost]  // TODO: Roles - Authorize(Roles = Roles.Administrator) 
    // public async Task<QuotationDto.RequestForQuotation> SubmitRequestForQuotation(QuotationDto.RequestForQuotation requestForQuotation)
    // {
    //     var QuotationVersion = await quotationService.SubmitRequestForQuotation(requestForQuotation.QuotationVersion);
    //     return CreatedAtAction(nameof(Create), requestForQuotation.QuotationVersion) ;
    // }
    [SwaggerOperation("Create a new Quotation")]
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




}
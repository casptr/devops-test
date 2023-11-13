using Foodtruck.Client.Supplements;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Formulas;
using Services.Supplements;
using Swashbuckle.AspNetCore.Annotations;

namespace Foodtruck.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormulaController : Controller
{
    private readonly IFormulaService formulaService;

    public FormulaController(IFormulaService formulaService)
    {
        this.formulaService = formulaService;
    }

    [SwaggerOperation("Returns all formulas.")]
    [HttpGet("all"), AllowAnonymous]
    public async Task<FormulaResult.Index> GetAll()
    {
        return await formulaService.GetAllAsync();
    }

    [SwaggerOperation("Returns a specific formula.")]
    [HttpGet("{formulaId}"), AllowAnonymous]
    public async Task<FormulaDto.Detail> GetDetail(int formulaId)
    {
        return await formulaService.GetDetailAsync(formulaId);
    }

    //[SwaggerOperation("Creates a new formula.")]
    //[HttpPost]  // TODO: Roles - Authorize(Roles = Roles.Administrator) 
    //public async Task<IActionResult> Create(FormulaDto.Mutate model)
    //{
    //    var formulaId = await formulaService.CreateAsync(model);
    //    return CreatedAtAction(nameof(Create), formulaId);
    //}

    //[SwaggerOperation("Edites an existing formula.")]
    //[HttpPut("{formulaId}")] // TODO: Roles - Authorize(Roles = Roles.Administrator)
    //public async Task<IActionResult> Edit(int formulaId, FormulaDto.Mutate model)
    //{
    //    await formulaService.EditAsync(formulaId, model);
    //    return NoContent();
    //}

    //[SwaggerOperation("Deletes an existing formula.")]
    //[HttpDelete("{formulaId}")] // TODO: Roles - Authorize(Roles = Roles.Administrator)
    //public async Task<IActionResult> Delete(int formulaId)
    //{
    //    await formulaService.DeleteAsync(formulaId);
    //    return NoContent();
    //}

    //[SwaggerOperation("Add supplementline to existing formula.")]
    //[HttpPost("{formulaId}/addsuppline")]
    //public async Task<IActionResult> AddSupplementLine(int formulaId)
    //{
    //    await formulaService.AddFormulaSupplementLine(formulaId);
    //    return CreatedAtAction(nameof(Create),  formulaId);
    //}

    //[SwaggerOperation("Add supplement choice to existing formula.")]
    //[HttpPost("{formulaId}/addsuppchoice")]
    //public async Task<IActionResult> AddSupplementChoice(int formulaId)
    //{
    //    await formulaService.AddFormulaSupplementChoice(formulaId);
    //    return CreatedAtAction(nameof(Create), formulaId);
    //}

}

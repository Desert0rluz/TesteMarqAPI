using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Business.Interfaces;
using Prova.MarQ.Domain.DTOs;

namespace Prova.MarQ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;
    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyService.GetAllCompaniesAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var company = await _companyService.GetCompanyByIdAsync(id);
        return company != null ? Ok(company) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CompanyDTO companyDto)
    {
        await _companyService.AddCompanyAsync(companyDto);
        return CreatedAtAction(nameof(GetById), new {id = companyDto.Id}, companyDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CompanyDTO companyDto)
    {
        await _companyService.UpdateCompanyAsync(id, companyDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _companyService.DeleteCompanyAsync(id);
        return NoContent();
    }
}

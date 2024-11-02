using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Business.Interfaces;
using Prova.MarQ.Domain.DTOs;

namespace Prova.MarQ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        return employee != null ? Ok(employee) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] EmployeeDTO employeeDto)
    {
        await _employeeService.AddEmployeeAsync(employeeDto);
        return CreatedAtAction(nameof(GetById), new { id = employeeDto.Id }, employeeDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeDTO employeeDto)
    {
        await _employeeService.UpdateEmployeeAsync(id, employeeDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _employeeService.DeleteEmployeeAsync(id);
        return NoContent();
    }
}

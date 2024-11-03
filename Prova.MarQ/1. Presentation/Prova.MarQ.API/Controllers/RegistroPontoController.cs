using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Business.Interfaces;

namespace Prova.MarQ.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistroPontoController : ControllerBase
{
    private readonly IRegistroPontoService _registroPontoService;

    public RegistroPontoController(IRegistroPontoService registroPontoService)
    {
        _registroPontoService = registroPontoService;
    }

    [HttpPost]
    public async Task<ActionResult<RegistroPonto>> RegistrarPonto(int pin, int entradaSaida)
    {
        var registro = await _registroPontoService.RegistrarPontoAsync(pin, entradaSaida);
        return CreatedAtAction(nameof(RegistrarPonto), new { id = registro.Id }, registro);
    }

    [HttpGet("funcionario/{id}")]
    public async Task<ActionResult<IQueryable<RegistroPonto>>> ObterRegistrosPorFuncionario(
        Guid id, DateTime dataInicio, DateTime dataFim)
    {
        if (dataInicio == default || dataFim == default)
        {
            return BadRequest("Data de início e data de fim são obrigatórias.");
        }

        var registros = await _registroPontoService.ObterRegistrosPorFuncionarioAsync(id, dataInicio, dataFim);
        return Ok(registros);
    }

    [HttpGet("empresa/{id}")]
    public async Task<ActionResult<IQueryable<RegistroPonto>>> ObterRegistrosPorEmpresa(
        Guid id, DateTime dataInicio, DateTime dataFim)
    {
        if (dataInicio == default || dataFim == default)
        {
            return BadRequest("Data de início e data de fim são obrigatórias.");
        }

        var registros = await _registroPontoService.ObterRegistrosPorEmpresaAsync(id, dataInicio, dataFim);
        return Ok(registros);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirRegistro(Guid id)
    {
        var result = await _registroPontoService.ExcluirRegistroAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}

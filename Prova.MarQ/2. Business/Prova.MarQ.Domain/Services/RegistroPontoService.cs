using Prova.MarQ.Business.Interfaces;
using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.Business.Services;

public class RegistroPontoService : IRegistroPontoService
{
    private readonly IRegistroPontoRepository _registroPontoRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public RegistroPontoService(IRegistroPontoRepository registroPontoRepository, IEmployeeRepository employeeRepository)
    {
        _registroPontoRepository = registroPontoRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<RegistroPonto> RegistrarPontoAsync(int pin, int entradaSaida)
    {
        var employee = await _employeeRepository.GetByPinAsync(pin);
        if (employee == null)
        {
            throw new Exception("Funcionário não encontrado.");
        }

        var registroPonto = new RegistroPonto
        {
            Ponto = DateTime.Now,
            IdEmployee = employee.Id, 
            IdCompany = employee.CompanyId,
            EntradaSaida = (EntradaSaida)entradaSaida, 
            IsDeleted = false
        };

        await _registroPontoRepository.AddAsync(registroPonto);

        return registroPonto;
    }

    public async Task<IQueryable<RegistroPonto>> ObterRegistrosPorFuncionarioAsync(Guid idFuncionario, DateTime dataInicio, DateTime dataFim)
    {
        var registros = await _registroPontoRepository.GetByEmployeeIdAsync(idFuncionario);
        return registros.Where(r => r.Ponto.Date >= dataInicio && r.Ponto.Date <= dataFim);
    }

    public async Task<IQueryable<RegistroPonto>> ObterRegistrosPorEmpresaAsync(Guid idEmpresa, DateTime dataInicio, DateTime dataFim)
    {
        var registros = await _registroPontoRepository.GetByCompanyIdAsync(idEmpresa);
        return registros.Where(r => r.Ponto.Date >= dataInicio && r.Ponto.Date <= dataFim);
    }

    public async Task<bool> ExcluirRegistroAsync(Guid idRegistro)
    {
        return await _registroPontoRepository.SoftDeleteAsync(idRegistro);
    }
}
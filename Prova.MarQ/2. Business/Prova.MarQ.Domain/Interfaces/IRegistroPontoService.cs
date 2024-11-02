using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.Business.Interfaces;

public interface IRegistroPontoService
{
    Task<RegistroPonto> RegistrarPontoAsync(int pin, int entradaSaida);
    Task<IQueryable<RegistroPonto>> ObterRegistrosPorFuncionarioAsync(Guid idFuncionario);
    Task<IQueryable<RegistroPonto>> ObterRegistrosPorEmpresaAsync(Guid idEmpresa);
    Task<bool> ExcluirRegistroAsync(Guid idRegistro);
}

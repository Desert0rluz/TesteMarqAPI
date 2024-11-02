using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.Business.Interfaces;

public interface IRegistroPontoRepository
{
    Task<RegistroPonto> AddAsync(RegistroPonto registroPonto);
    Task<IQueryable<RegistroPonto>> GetByEmployeeIdAsync(Guid employeeId);
    Task<IQueryable<RegistroPonto>> GetByCompanyIdAsync(Guid companyId);
    Task<RegistroPonto> GetByIdAsync(Guid id);
    Task<bool> SoftDeleteAsync(Guid id);
}

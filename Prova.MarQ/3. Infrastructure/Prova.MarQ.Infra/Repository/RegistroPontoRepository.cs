using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Business.Interfaces;

namespace Prova.MarQ.Infra.Repositories;

public class RegistroPontoRepository : IRegistroPontoRepository
{
    private readonly ProvaMarqDbContext _context;

    public RegistroPontoRepository(ProvaMarqDbContext context)
    {
        _context = context;
    }
    public async Task<RegistroPonto> AddAsync(RegistroPonto registroPonto)
    {
        await _context.RegistroPontos.AddAsync(registroPonto);
        await _context.SaveChangesAsync();
        return registroPonto;
    }
    public async Task<IQueryable<RegistroPonto>> GetByEmployeeIdAsync(Guid employeeId)
    {
        return _context.RegistroPontos
            .Where(r => r.IdEmployee == employeeId && !r.IsDeleted);
    }
    public async Task<IQueryable<RegistroPonto>> GetByCompanyIdAsync(Guid companyId)
    {
        return _context.RegistroPontos
            .Where(r => r.IdCompany == companyId && !r.IsDeleted);
    }
    public async Task<RegistroPonto?> GetByIdAsync(Guid id)
    {
        return await _context.RegistroPontos.FindAsync(id);
    }
    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var registro = await GetByIdAsync(id);
        if (registro == null)
        {
            return false;
        }

        registro.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
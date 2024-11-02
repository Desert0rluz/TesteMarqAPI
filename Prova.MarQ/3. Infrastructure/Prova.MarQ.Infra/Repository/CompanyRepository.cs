using Microsoft.EntityFrameworkCore;
using Prova.MarQ.Business.Interfaces;
using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.Infra.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ProvaMarqDbContext _context;

    public CompanyRepository(ProvaMarqDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await _context.Companies.ToListAsync();
    }
    public async Task<Company?> GetByIdAsync(Guid id)
    {
        return await _context.Companies.FindAsync(id);
    }
    public async Task AddAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
    }
    public async Task SoftDeleteAsync(Guid id)
    {
        var company = await GetByIdAsync(id);
        if (company != null)
        {
            company.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
    public async Task<bool> DocumentExistsAsync(string document)
    {
        return await _context.Companies.AnyAsync(c => c.Document == document);
    }
    public async Task<bool> ExistsAsync(Guid companyId)
    {
        return await _context.Companies.AnyAsync(c => c.Id == companyId && !c.IsDeleted);
    }
}

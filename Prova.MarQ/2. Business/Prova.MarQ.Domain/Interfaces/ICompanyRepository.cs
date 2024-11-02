using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.Business.Interfaces;
public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(Guid id);
    Task AddAsync(Company company);
    Task UpdateAsync(Company company);
    Task SoftDeleteAsync(Guid id);
    Task<bool> DocumentExistsAsync(string document);
    Task<bool> ExistsAsync(Guid companyId);
}
using Prova.MarQ.Domain.DTOs;

namespace Prova.MarQ.Business.Interfaces;
public interface ICompanyService
{
    Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync();
    Task<CompanyDTO?> GetCompanyByIdAsync(Guid id);
    Task AddCompanyAsync(CompanyDTO companyDto);
    Task UpdateCompanyAsync(Guid id, CompanyDTO companyDto);
    Task DeleteCompanyAsync(Guid id);
}

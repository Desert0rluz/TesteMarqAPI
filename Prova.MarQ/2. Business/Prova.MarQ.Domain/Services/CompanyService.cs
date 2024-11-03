using Prova.MarQ.Business.Interfaces;
using Prova.MarQ.Domain.DTOs;
using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.Business.Services;
public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return companies.Select(c => new CompanyDTO
        {
            Id = c.Id,
            Name = c.Name,
            Document = c.Document
        });
    }
    public async Task<CompanyDTO> GetCompanyByIdAsync(Guid id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        return company == null ? null : new CompanyDTO
        {
            Id = company.Id,
            Name = company.Name,
            Document = company.Document
        };
    }
    public async Task AddCompanyAsync(CompanyDTO companyDto)
    {
        if (await _companyRepository.DocumentExistsAsync(companyDto.Document))
            throw new InvalidOperationException("Documento já existe.");

        var company = new Company
        {
            Name = companyDto.Name,
            Document = companyDto.Document
        };

        await _companyRepository.AddAsync(company);
    }
    public async Task UpdateCompanyAsync(Guid id, CompanyDTO companyDto)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
            throw new KeyNotFoundException("Empresa não encontrada.");

        if (companyDto.Document != null && companyDto.Document != company.Document)
        {
            if (await _companyRepository.DocumentExistsAsync(companyDto.Document))
                throw new InvalidOperationException("Documento já registrado.");

            company.Document = companyDto.Document;
        }
        if (!string.IsNullOrEmpty(companyDto.Name))
        {
            company.Name = companyDto.Name; 
        }
        await _companyRepository.UpdateAsync(company);
    }
    public async Task DeleteCompanyAsync(Guid id)
    {
        await _companyRepository.SoftDeleteAsync(id);
    }
}

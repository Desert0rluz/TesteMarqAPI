using Prova.MarQ.Business.Interfaces;
using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Domain.DTOs;

namespace Prova.MarQ.Business.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICompanyRepository _companyRepository;
    public EmployeeService(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository)
    {
        _employeeRepository = employeeRepository;
        _companyRepository = companyRepository;
    }
    public async Task<EmployeeDTO> GetByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null || employee.IsDeleted)
        {
            throw new KeyNotFoundException("Funcionário não encontrado ou foi excluído.");
        }

        return new EmployeeDTO
        {
            Id = employee.Id,
            Name = employee.Name,
            PIN = employee.PIN,
            CompanyId = employee.CompanyId
        };
    }
    public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees
            .Where(e => !e.IsDeleted)
            .Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name,
                PIN = e.PIN,
                CompanyId = e.CompanyId
            })
            .ToList();
    }
    public async Task AddEmployeeAsync(EmployeeDTO employeeDto)
    {
        if (await _employeeRepository.PinExistsAsync(employeeDto.PIN))
            throw new InvalidOperationException("O PIN já está em uso por outro funcionário.");

        if (employeeDto.CompanyId == Guid.Empty)
            throw new ArgumentException("O funcionário deve estar vinculado a uma empresa válida.");

        if (!await _companyRepository.ExistsAsync(employeeDto.CompanyId))
            throw new KeyNotFoundException("A empresa vinculada não existe.");

        var employee = new Employee
        {
            Name = employeeDto.Name,
            PIN = employeeDto.PIN,
            CompanyId = employeeDto.CompanyId,
            IsDeleted = false
        };

        await _employeeRepository.AddAsync(employee);
    }

    public async Task UpdateEmployeeAsync(Guid id, EmployeeDTO employeeDto)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null || employee.IsDeleted)
        {
            throw new KeyNotFoundException("Funcionário não encontrado ou foi excluído.");
        }

        if (!string.IsNullOrWhiteSpace(employeeDto.Name))
        {
            employee.Name = employeeDto.Name;
        }

        if (employeeDto.PIN != 0 && employeeDto.PIN != employee.PIN)
        {
            if (await _employeeRepository.PinExistsAsync(employeeDto.PIN))
            {
                throw new InvalidOperationException("O PIN informado já está em uso por outro funcionário.");
            }
            employee.PIN = employeeDto.PIN;
        }

        if (employeeDto.CompanyId != Guid.Empty && employeeDto.CompanyId != employee.CompanyId)
        {
            if (!await _companyRepository.ExistsAsync(employeeDto.CompanyId))
            {
                throw new InvalidOperationException("A empresa informada não existe.");
            }
            employee.CompanyId = employeeDto.CompanyId;
        }

        await _employeeRepository.UpdateAsync(employee);
    }

    public async Task DeleteEmployeeAsync(Guid id)
    {
        await _employeeRepository.DeleteAsync(id);
    }
}

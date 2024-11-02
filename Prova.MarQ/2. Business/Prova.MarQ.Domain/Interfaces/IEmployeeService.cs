using Prova.MarQ.Domain.DTOs;

namespace Prova.MarQ.Business.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<EmployeeDTO>> GetAllAsync();
    Task AddEmployeeAsync(EmployeeDTO employeeDto);
    Task UpdateEmployeeAsync(Guid id, EmployeeDTO employeeDto);
    Task DeleteEmployeeAsync(Guid id);
}

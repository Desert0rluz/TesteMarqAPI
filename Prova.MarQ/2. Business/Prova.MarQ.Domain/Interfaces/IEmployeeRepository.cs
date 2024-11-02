using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.Business.Interfaces;
public interface IEmployeeRepository
{
    Task<Employee> GetByIdAsync(Guid id);
    Task<Employee> GetByPinAsync(int PIN);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Guid id);
    Task<bool> PinExistsAsync(int pin);
}

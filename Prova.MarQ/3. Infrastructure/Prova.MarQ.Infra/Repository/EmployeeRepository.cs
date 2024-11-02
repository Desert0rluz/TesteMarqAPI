using Microsoft.EntityFrameworkCore;
using Prova.MarQ.Business.Interfaces;
using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra;

namespace Prova.MarQ.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ProvaMarqDbContext _context;

    public EmployeeRepository(ProvaMarqDbContext context)
    {
        _context = context;
    }
    public async Task<Employee> GetByIdAsync(Guid id)
    {
        return await _context.Employees
            .Include(e => e.Company)
            .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    }
    public async Task<Employee> GetByPinAsync(int pin)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.PIN == pin);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .Where(e => !e.IsDeleted) 
            .ToListAsync();
    }
    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync(); 
    }
    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var employee = await GetByIdAsync(id);
        if (employee != null)
        {
            employee.IsDeleted = true;
            await UpdateAsync(employee);
        }
    }
    public async Task<bool> PinExistsAsync(string pin)
    {
        return await _context.Employees
            .AnyAsync(e => e.PIN.ToString() == pin && !e.IsDeleted);
    }
    public async Task<bool> PinExistsAsync(int pin)
    {
        return await _context.Employees.AnyAsync(e => e.PIN == pin && !e.IsDeleted);
    }
}

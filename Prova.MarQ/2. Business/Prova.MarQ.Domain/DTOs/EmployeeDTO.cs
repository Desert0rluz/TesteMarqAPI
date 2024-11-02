namespace Prova.MarQ.Domain.DTOs;

public class EmployeeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int PIN { get; set; }
    public Guid CompanyId { get; set; }
}

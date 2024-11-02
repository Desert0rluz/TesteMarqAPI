namespace Prova.MarQ.Domain.Entities;

public class Company : Base
{
    public string Name { get; set; }
    public string Document { get; set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<Employee> Employees {get; set; } = new List<Employee>();
    public ICollection<RegistroPonto> RegistroPontos { get; set; } = new List<RegistroPonto>();
}

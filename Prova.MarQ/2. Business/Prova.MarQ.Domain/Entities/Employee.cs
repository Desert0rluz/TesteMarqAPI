using System.Text.Json.Serialization;

namespace Prova.MarQ.Domain.Entities;

public class Employee : Base
{
    public string Name { get; set; }
    public string Document { get; set; }
    public int PIN { get; set; }
    public bool IsDeleted { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }

    [JsonIgnore]
    public ICollection<RegistroPonto> RegistroPontos { get; set; } = new List<RegistroPonto>();
}
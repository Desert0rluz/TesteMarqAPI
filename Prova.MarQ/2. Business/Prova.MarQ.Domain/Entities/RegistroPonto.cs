namespace Prova.MarQ.Domain.Entities;

public class RegistroPonto : Base
{
    public DateTime Ponto { get; set; }
    public Guid? IdEmployee { get; set; }
    public Employee Employee { get; set; }
    public Guid? IdCompany { get; set; }
    public Company Company { get; set; }
    public bool IsDeleted { get; set; }
    public EntradaSaida EntradaSaida { get; set; } 
}
public enum EntradaSaida
{
    Entrada = 0,
    Saida = 1
}

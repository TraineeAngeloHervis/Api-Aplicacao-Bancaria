using Crosscutting.Enums;

namespace Domain.Entities;

public class Cliente
{
    public Guid Id { get; init; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public EstadoCivil EstadoCivil { get; set; }
    public ICollection<Conta> Contas { get; set; }
}
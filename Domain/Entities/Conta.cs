using Crosscutting.Enums;

namespace Domain.Entities;

public class Conta
{
    public Guid Id { get; init; }
    public Guid ClienteId { get; init; }
    public Cliente Cliente { get; set; }
    public decimal SaldoInicial { get; set; }
    public DateTime DataAbertura { get; set; } = DateTime.Now;
    public TipoConta TipoConta { get; set; }
}
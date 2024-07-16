using Crosscutting.Enums;

namespace Domain.Entities;

public class Conta
{
<<<<<<< Updated upstream
    public Guid Id { get; init; }
    public Guid ClienteId { get; init; }
=======
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
>>>>>>> Stashed changes
    public Cliente Cliente { get; set; }
    public decimal SaldoInicial { get; set; }
    public DateTime DataAbertura { get; set; } = DateTime.Now;
    public TipoConta TipoConta { get; set; }
}
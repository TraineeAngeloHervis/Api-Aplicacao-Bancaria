using Crosscutting.Enums;

namespace Domain.Entities;

public class Conta
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public decimal Saldo { get; set; }
    public DateTime DataAbertura { get; set; } = DateTime.Now;
    public TipoConta TipoConta { get; set; }
    public ICollection<Transacao> Transacoes { get; set; }
}
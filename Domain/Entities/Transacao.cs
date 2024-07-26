namespace Domain.Entities;

public class Transacao
{
    public Guid Id { get; set; }
    public Guid ContaOrigemId { get; set; }
    public Conta Conta { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataTransacao { get; set; } = DateTime.Now;
}
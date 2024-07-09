namespace Domain.Contas.Entities;

public class Conta
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public decimal SaldoInicial { get; set; }
    public DateTime DataAbertura { get; set; } = DateTime.Now;
    public string TipoConta { get; set; }
}
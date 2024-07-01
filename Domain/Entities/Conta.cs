namespace Domain.Entities;

public abstract class Conta
{
    protected Conta() => Id = Guid.NewGuid();
    public Guid Id { get; }
    public Guid IdCliente { get; }
    public int NumeroConta { get; init; }
    public DateTime DataAbertura { get; private set; } = DateTime.Now;
    public decimal SaldoInicial { get; private set; }
    public string TipoConta { get; private set; }
}
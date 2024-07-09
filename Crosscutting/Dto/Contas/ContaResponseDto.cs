namespace Crosscutting.Dto.Contas;

public class ContaResponseDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public decimal SaldoInicial { get; set; }
    public string TipoConta { get; set; }
    public DateTime DataAbertura { get; set; }
}
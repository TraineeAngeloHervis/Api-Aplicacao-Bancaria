namespace Crosscutting.Dto.Contas;

public class ContaResponseDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public decimal Saldo { get; set; }
    public string TipoConta { get; set; }
    public DateTime DataAbertura { get; set; }
}
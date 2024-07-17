using Crosscutting.Enums;

namespace Crosscutting.Dto;

public class ContaRequestDto
{
    public Guid ClienteId { get; set; }
    public decimal SaldoInicial { get; set; }
    public TipoConta TipoConta { get; set; }
    public DateTime DataAbertura { get; set; } = DateTime.Now;
}
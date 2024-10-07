using Crosscutting.Enums;

namespace Crosscutting.Dto;

public class DepositoRequestDto
{
    public Guid ContaOrigemId { get; set; }
    public decimal Valor { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
}
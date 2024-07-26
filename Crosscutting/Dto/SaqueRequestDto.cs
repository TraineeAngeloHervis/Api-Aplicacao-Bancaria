using Crosscutting.Enums;

namespace Crosscutting.Dto;

public class SaqueRequestDto
{
    public Guid ContaOrigemId { get; set; }
    public decimal Valor { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
}
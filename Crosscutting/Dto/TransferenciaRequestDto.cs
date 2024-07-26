using Crosscutting.Enums;

namespace Crosscutting.Dto;

public class TransferenciaRequestDto
{
    public Guid ContaOrigemId { get; set; }
    public Guid ContaDestinoId { get; set; }
    public decimal Valor { get; set; }
}
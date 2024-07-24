using Crosscutting.Enums;

namespace Crosscutting.Dto;

public class TransacaoRequestDto
{
    public Guid ContaOrigemId { get; set; }
    public Guid ContaDestinoId { get; set; }
    public decimal Valor { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
}
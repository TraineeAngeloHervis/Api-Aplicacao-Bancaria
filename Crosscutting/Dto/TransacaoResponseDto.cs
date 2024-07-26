using Crosscutting.Enums;

namespace Crosscutting.Dto;

public class TransacaoResponseDto
{
    public Guid Id { get; set; }
    public Guid ContaOrigemId { get; set; }
    public Guid? ContaDestinoId { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataTransacao { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
}
#nullable enable
using Crosscutting.Enums;

namespace Domain.Entities;

public class Transacao
{
    public Guid Id { get; set; }
    public Guid ContaOrigemId { get; set; }
    public Conta ContaOrigem { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataTransacao { get; set; } = DateTime.Now;
    public Guid? ContaDestinoId { get; set; }
    public Conta? ContaDestino { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
}
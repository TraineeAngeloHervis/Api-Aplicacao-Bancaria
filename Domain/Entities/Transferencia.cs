namespace Domain.Entities;

public class Transferencia : Transacao
{
    public Guid ContaDestinoId { get; set; }
    public Conta ContaDestino { get; set; }
}
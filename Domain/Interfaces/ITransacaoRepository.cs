using Domain.Entities;

namespace Domain.Interfaces;

public interface ITransacaoRepository
{
    Task<Transacao> SalvarTransacao(Transacao transacao);
    Task<Conta> AtualizarSaldo(Guid contaId, decimal valor);
    Task<Conta> ConsultarConta(Guid contaId);
    Task<IEnumerable<Transacao>> GerarExtrato(Guid contaId);
    Task<Transacao> ConsultarTransacao(Guid id);
}
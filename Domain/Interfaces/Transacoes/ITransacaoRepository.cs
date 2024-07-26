using Domain.Entities;

namespace Domain.Interfaces.Transacoes;

public interface ITransacaoRepository
{
    Task<Transacao> SalvarTransacao(Transacao transacao);
    Task<Conta> AtualizarSaldo(Guid contaOrigemId, decimal valor);
    Task<Conta> ConsultarConta(Guid contaOrigemId);
    Task<IEnumerable<Transacao>> GerarExtrato(Guid contaOrigemId);
    Task<Transacao> ConsultarTransacao(Guid id);
}
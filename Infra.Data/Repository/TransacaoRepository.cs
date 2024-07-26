using Domain.Entities;
using Domain.Interfaces.Transacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infra.Data.Repository;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transacao> SalvarTransacao(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();
        return transacao;
    }

    public async Task<Conta> AtualizarSaldo(Guid contaOrigemId, decimal valor)
    {
        var conta = await _context.Contas.FindAsync(contaOrigemId);
        if (conta == null) return null;
        conta.Saldo += valor;
        _context.Contas.Update(conta);
        await _context.SaveChangesAsync();

        return conta;
    }

    public async Task<Conta> ConsultarConta(Guid contaOrigemId)
    {
        return await _context.Contas.FindAsync(contaOrigemId);
    }

    public async Task<IDbContextTransaction> CriarTransacaoAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task<Transacao> ConsultarTransacao(Guid id)
    {
        return await _context.Transacoes.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transacao>> GerarExtrato(Guid contaOrigemId)
    {
        return await _context.Transacoes
            .Where(t => t.ContaOrigemId == contaOrigemId)
            .ToListAsync();
    }
}
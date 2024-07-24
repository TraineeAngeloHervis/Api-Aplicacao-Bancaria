using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Conta> AtualizarSaldo(Guid contaId, decimal valor)
    {
        var conta = await _context.Contas.FindAsync(contaId);
        if (conta == null) return null;
        conta.Saldo += valor;
        _context.Contas.Update(conta);
        await _context.SaveChangesAsync();

        return conta;
    }

    public async Task<Conta> ConsultarConta(Guid contaId)
    {
        return await _context.Contas.FindAsync(contaId);
    }

    public async Task<IEnumerable<Transacao>> GerarExtrato(Guid contaId)
    {
        return await _context.Transacoes.Where(t => 
                t.ContaOrigemId == contaId || 
                t.ContaDestinoId == contaId)
            .ToListAsync();
    }
    
    public async Task<Transacao> ConsultarTransacao(Guid id)
    {
        return await _context.Transacoes.FindAsync(id);
    }
}
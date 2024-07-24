using Crosscutting.Enums;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class ContaRepository : IContaRepository
{
    private readonly AppDbContext _context;

    public ContaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Conta> CadastrarConta(Guid clienteId, Conta conta)
    {
        conta.ClienteId = clienteId;
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();
        return conta;
    }

    public async Task<Conta> AtualizarConta(Guid clienteId, Conta conta, Guid id)
    {
        conta.ClienteId = clienteId;
        conta.Id = id;
        _context.Contas.Update(conta);
        await _context.SaveChangesAsync();
        return conta;
    }

    public async Task<bool> ExcluirConta(Guid id)
    {
        var conta = await _context.Contas.FindAsync(id);
        if (conta is null) return false;
        _context.Contas.Remove(conta);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Conta> ConsultarConta(Guid id)
    {
        return await _context.Contas.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Conta>> ListarContas(Guid clienteId)
    {
        return await _context.Contas.Where(c => c.ClienteId == clienteId).ToListAsync();
    }
}
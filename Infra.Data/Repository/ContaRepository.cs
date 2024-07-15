using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class ContaRepository(AppDbContext context) : IContaRepository
{
    public async Task<Conta> CadastrarConta(Guid clienteId, Conta conta)
    {
        await context.Contas.AddAsync(conta);
        await context.SaveChangesAsync();
        return conta;
    }

    public async Task<Conta> AtualizarConta(Guid clienteId, Conta conta)
    {
        context.Contas.Update(conta);
        await context.SaveChangesAsync();
        return conta;
    }

    public async Task<bool> ExcluirConta(Guid clienteId, Guid id)
    {
        var conta = await context.Contas.FindAsync(id);
        if (conta == null) return false;

        context.Contas.Remove(conta);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Conta> ConsultarConta(Guid clienteId, Guid id)
    {
        return await context.Contas.FindAsync(id);
    }

    public async Task<IEnumerable<Conta>> ListarContas(Guid clienteId)
    {
        return await context.Contas.Where(c => c.ClienteId == clienteId).ToListAsync();
    }
}
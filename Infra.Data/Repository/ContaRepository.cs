using Domain.Entities;
using Domain.Interfaces;
<<<<<<< Updated upstream
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> Stashed changes

namespace Infra.Data.Repository;

public class ContaRepository(AppDbContext context) : IContaRepository
{
<<<<<<< Updated upstream
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
=======
    public Conta CadastrarConta(Guid clienteId, Conta conta)
    {
        conta.ClienteId = clienteId;
        context.Contas.Add(conta);
        context.SaveChanges();
        return conta;
    }

    public Conta AtualizarConta(Guid clienteId, Conta conta)
    {
        var contaDb = context.Contas.FirstOrDefault(c => c.Id == conta.Id && c.ClienteId == clienteId);
        if (contaDb == null) return null;

        contaDb.SaldoInicial = conta.SaldoInicial;
        contaDb.TipoConta = conta.TipoConta;

        context.Contas.Update(contaDb);
        context.SaveChanges();
        return contaDb;
    }

    public bool ExcluirConta(Guid clienteId, Guid id)
    {
        var contaDb = context.Contas.FirstOrDefault(c => c.Id == id && c.ClienteId == clienteId);
        if (contaDb == null) return false;

        context.Contas.Remove(contaDb);
        context.SaveChanges();
        return true;
    }

    public Conta ConsultarConta(Guid clienteId, Guid id)
    {
        return context.Contas.FirstOrDefault(c => c.Id == id && c.ClienteId == clienteId);
    }

    public IEnumerable<Conta> ListarContas(Guid clienteId)
    {
        return context.Contas.Where(c => c.ClienteId == clienteId).ToList();
>>>>>>> Stashed changes
    }
}
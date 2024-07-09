using Domain.Contas.Entities;
using Domain.Contas.Interfaces;

namespace Infra.Data.Repository;

public class ContaRepository(AppDbContext context) : IContaRepository
{
    public Conta CadastrarConta(Conta conta)
    {
        context.Contas.Add(conta);
        context.SaveChanges();
        return conta;
    }

    public Conta AtualizarConta(Conta conta)
    {
        context.Contas.Update(conta);
        context.SaveChanges();
        return conta;
    }

    public bool ExcluirConta(Guid id)
    {
        var conta = context.Contas.Find(id);
        if (conta is null) return false;
        context.Contas.Remove(conta);
        context.SaveChanges();
        return true;
    }

    public Conta ConsultarConta(Guid id)
    {
        return context.Contas.Find(id);
    }

    public IEnumerable<Conta> ListarContas()
    {
        return context.Contas.ToList();
    }
}
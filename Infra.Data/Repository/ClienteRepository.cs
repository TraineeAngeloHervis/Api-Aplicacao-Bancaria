using Domain.Entities;
using Domain.Interfaces;

namespace Infra.Data.Repository;

public class ClienteRepository(AppDbContext context) : IClienteRepository
{
    public Cliente CadastrarCliente(Cliente cliente)
    {
        context.Clientes.Add(cliente);
        context.SaveChanges();
        return cliente;
    }

    public Cliente AtualizarCliente(Cliente cliente)
    {
        context.Clientes.Update(cliente);
        context.SaveChanges();
        return cliente;
    }

    public bool ExcluirCliente(Guid id)
    {
        var cliente = context.Clientes.Find(id);
        if (cliente is null) return false;
        context.Clientes.Remove(cliente);
        context.SaveChanges();
        return true;
    }

    public Cliente ConsultarCliente(Guid id)
    {
        return context.Clientes.FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Cliente> ListarClientes()
    {
        return context.Clientes.ToList();
    }
}
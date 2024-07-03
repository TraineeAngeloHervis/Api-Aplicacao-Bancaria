using Domain.Clientes.Entities;

namespace Infra.Data.Repository;

public class ClienteRepository(AppDbContext context)
{
    public Cliente? CadastrarCliente(Cliente? cliente)
    {
        context.Clientes.Add(cliente);
        context.SaveChanges();
        return cliente;
    }
    
    public Cliente? AtualizarCliente(Cliente? cliente)
    {
        context.Clientes.Update(cliente);
        context.SaveChanges();
        return cliente;
    }
    
    public bool ExcluirCliente(Cliente cliente)
    {
        context.Clientes.Remove(cliente);
        context.SaveChanges();
        return true;
    }
    
    public Cliente? ConsultarCliente(Guid id)
    {
        return context.Clientes.Find(id);
    }
    
    public IEnumerable<Cliente?> ListarClientes()
    {
        return context.Clientes.ToList();
    }
}
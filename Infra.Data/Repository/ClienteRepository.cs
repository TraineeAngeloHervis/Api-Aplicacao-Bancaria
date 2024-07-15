using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class ClienteRepository(AppDbContext context) : IClienteRepository
{
    public async Task<Cliente> CadastrarCliente(Cliente cliente)
    {
        await context.Clientes.AddAsync(cliente);
        await context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> AtualizarCliente(Cliente cliente)
    {
        context.Clientes.Update(cliente);
        await context.SaveChangesAsync();
        return cliente;
    }
    
    public async Task<bool> ExcluirCliente(Guid id)
    {
        var cliente = await context.Clientes.FindAsync(id);
        if (cliente == null) return false;

        context.Clientes.Remove(cliente);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Cliente> ConsultarCliente(Guid id)
    {
        return await context.Clientes.FindAsync(id);
    }

    public async Task<IEnumerable<Cliente>> ListarClientes()
    {
        return await context.Clientes.ToListAsync();
    }
}
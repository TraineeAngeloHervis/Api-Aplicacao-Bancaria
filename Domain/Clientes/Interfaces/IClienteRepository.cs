using Domain.Clientes.Entities;

namespace Domain.Clientes.Interfaces;

public interface IClienteRepository
{
    Cliente CadastrarCliente(Cliente cliente);
    Cliente AtualizarCliente(Cliente cliente);
    bool ExcluirCliente(Guid id);
    Cliente ConsultarCliente(Guid id);
    IEnumerable<Cliente> ListarClientes();
}
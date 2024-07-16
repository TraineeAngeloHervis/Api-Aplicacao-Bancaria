using Domain.Entities;

namespace Domain.Interfaces;

public interface IClienteRepository
{
    Task <Cliente> CadastrarCliente(Cliente cliente);
    Cliente AtualizarCliente(Cliente cliente);
    bool ExcluirCliente(Guid id);
    Cliente ConsultarCliente(Guid id);
    IEnumerable<Cliente> ListarClientes();
}
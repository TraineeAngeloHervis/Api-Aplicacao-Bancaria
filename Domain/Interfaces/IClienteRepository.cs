using Domain.Entities;

namespace Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente> CadastrarCliente(Cliente cliente);
    Task<Cliente> AtualizarCliente(Cliente cliente);
    Task<bool> ExcluirCliente(Guid id);
    Task<Cliente> ConsultarCliente(Guid id);
    Task<IEnumerable<Cliente>> ListarClientes();
}
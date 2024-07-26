using Domain.Entities;

namespace Domain.Interfaces.Clientes;

public interface IClienteRepository
{
    Task<Cliente> CadastrarCliente(Entities.Cliente cliente);
    Task<Cliente> AtualizarCliente(Entities.Cliente cliente);
    Task<bool> ExcluirCliente(Guid id);
    Task<Entities.Cliente> ConsultarCliente(Guid id);
    Task<IEnumerable<Entities.Cliente>> ListarClientes();
}
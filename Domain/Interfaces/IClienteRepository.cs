using Domain.Entities;

namespace Domain.Interfaces;

public interface IClienteRepository
{
<<<<<<< Updated upstream
    Task<Cliente> CadastrarCliente(Cliente cliente);
    Task<Cliente> AtualizarCliente(Cliente cliente);
    Task<bool> ExcluirCliente(Guid id);
    Task<Cliente> ConsultarCliente(Guid id);
    Task<IEnumerable<Cliente>> ListarClientes();
=======
    Cliente CadastrarCliente(Cliente cliente);
    Cliente AtualizarCliente(Cliente cliente);
    bool ExcluirCliente(Guid id);
    Cliente ConsultarCliente(Guid id);
    IEnumerable<Cliente> ListarClientes();
>>>>>>> Stashed changes
}
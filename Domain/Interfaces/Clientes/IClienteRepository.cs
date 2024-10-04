using Crosscutting.Dto;
using Domain.Entities;

namespace Domain.Interfaces.Clientes;

public interface IClienteRepository
{
    Task<Cliente> CadastrarCliente(Cliente cliente);
    Task<Cliente> AtualizarCliente(Cliente cliente);
    Task<bool> ExcluirCliente(Guid id);
    Task<Cliente> ConsultarCliente(Guid id);
    Task<IEnumerable<Cliente>> ListarClientes();
    Task<IEnumerable<DadosContaDto>> ConsultarTransacoes(Guid id);
    Task<IEnumerable<Cliente>> ListarClientesComDapper();
    Task<IEnumerable<DadosContaDto>> ConsultarTransacoesDapper(Guid id);
}
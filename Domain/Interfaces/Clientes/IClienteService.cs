using Crosscutting.Dto;

namespace Domain.Interfaces.Clientes;

public interface IClienteService
{
    Task<ClienteResponseDto> CadastrarCliente(ClienteRequestDto clienteRequestDto);
    Task<ClienteResponseDto> AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto);
    Task<bool> ExcluirCliente(Guid id);
    Task<ClienteResponseDto> ConsultarCliente(Guid id);
    Task<IEnumerable<ClienteResponseDto>> ListarClientes();
    Task<IEnumerable<ClienteResponseDto>> ListarClientesComDapper();
    Task<IEnumerable<DadosContaDto>> ConsultarTransacoesDapper(Guid id);
}
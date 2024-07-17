using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface IClienteService
{
    //task é uma tarefa assíncrona
    Task<ClienteResponseDto> CadastrarCliente(ClienteRequestDto clienteRequestDto);
    Task<ClienteResponseDto> AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto);
    Task<bool> ExcluirCliente(Guid id);
    Task<ClienteResponseDto> ConsultarCliente(Guid id);
    Task<IEnumerable<ClienteResponseDto>> ListarClientes();
}
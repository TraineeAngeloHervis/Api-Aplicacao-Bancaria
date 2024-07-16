using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface IClienteService
{
<<<<<<< Updated upstream
    Task<ClienteResponseDto> CadastrarCliente(ClienteRequestDto clienteRequestDto);
    Task<ClienteResponseDto> AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto);
    Task<bool> ExcluirCliente(Guid id);
    Task<ClienteResponseDto> ConsultarCliente(Guid id);
    Task<IEnumerable<ClienteResponseDto>> ListarClientes();
=======
    ClienteResponseDto CadastrarCliente(ClienteRequestDto clienteRequestDto);
    ClienteResponseDto AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto);
    bool ExcluirCliente(Guid id);
    ClienteResponseDto ConsultarCliente(Guid id);
    IEnumerable<ClienteResponseDto> ListarClientes();
>>>>>>> Stashed changes
}
using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface IClienteService
{
    ClienteResponseDto CadastrarCliente(ClienteRequestDto clienteRequestDto);
    ClienteResponseDto AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto);
    bool ExcluirCliente(Guid id);
    ClienteResponseDto ConsultarCliente(Guid id);
    IEnumerable<ClienteResponseDto> ListarClientes();
}
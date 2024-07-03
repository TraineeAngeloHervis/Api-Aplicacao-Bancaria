using Crosscutting.Dto.Clientes;

namespace Domain.Clientes.Interfaces;

public interface IClienteService
{
    ClienteResponseDto CadastrarCliente(ClienteRequestDto clienteRequestDto);
    ClienteResponseDto AtualizarCliente(ClienteRequestDto clienteRequestDto);
    bool ExcluirCliente(Guid id);
    ClienteResponseDto ConsultarCliente(Guid id);
    IEnumerable<ClienteResponseDto> ListarClientes();
}
using Crosscutting.Dto;

namespace Domain.Interfaces.Clientes;

public interface IClienteValidator
{
    Task<bool> EhValido(ClienteRequestDto clienteRequestDto, out IList<string> errors);
}
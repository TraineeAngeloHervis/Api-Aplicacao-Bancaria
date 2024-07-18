using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface IClienteValidator
{
    bool EhValido(ClienteRequestDto clienteRequestDto, out IList<string> errors);
}
using Crosscutting.Validators;

namespace Domain.Interfaces;

public interface IClienteValidator
{
    Task<bool> EhValido(ClienteValidator clienteValidator);
}
using Crosscutting.Validators;

namespace Domain.Interfaces;

public interface IContaValidator
{
    Task<bool> EhValido(ContaValidator contaValidator);
}
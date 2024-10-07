using Crosscutting.Dto;

namespace Domain.Interfaces.Contas;

public interface IContaValidator
{
    Task<bool> EhValido(ContaRequestDto contaRequestDto, out IList<string> errors);
}
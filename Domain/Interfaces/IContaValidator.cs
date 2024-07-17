using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface IContaValidator
{
    bool EhValido(ContaRequestDto contaRequestDto, out IList<string> errors);
}
using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface ISaqueValidator
{
    bool EhValido(TransacaoRequestDto transacaoRequestDto, out IList<string> errors);
}
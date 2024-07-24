using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface ITransacaoValidator
{
    bool EhValido(TransacaoRequestDto transacaoRequestDto, out IList<string> errors);
}
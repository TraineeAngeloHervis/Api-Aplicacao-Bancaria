using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface ITransferenciaValidator
{
    bool EhValido(TransacaoRequestDto transacaoRequestDto, out IList<string> errors);
}
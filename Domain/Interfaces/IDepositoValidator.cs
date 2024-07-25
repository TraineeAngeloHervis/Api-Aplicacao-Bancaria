using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface IDepositoValidator
{
    bool EhValido(TransacaoRequestDto transacaoRequestDto, out IList<string> errors);
}
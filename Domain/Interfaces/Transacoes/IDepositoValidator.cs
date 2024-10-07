using Crosscutting.Dto;

namespace Domain.Interfaces.Transacoes;

public interface IDepositoValidator
{
    Task<bool> EhValido(DepositoRequestDto depositoRequestDto, out IList<string> errors);
}
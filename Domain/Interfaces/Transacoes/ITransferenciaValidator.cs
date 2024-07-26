using Crosscutting.Dto;

namespace Domain.Interfaces.Transacoes;

public interface ITransferenciaValidator
{
    Task<bool> EhValido(TransferenciaRequestDto transferenciaRequestDto, out IList<string> errors);
}
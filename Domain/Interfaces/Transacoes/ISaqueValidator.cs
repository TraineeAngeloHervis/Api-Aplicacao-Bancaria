using Crosscutting.Dto;

namespace Domain.Interfaces.Transacoes;

public interface ISaqueValidator
{
    Task<bool> EhValido(SaqueRequestDto saqueRequestDto, out IList<string> errors); 
}
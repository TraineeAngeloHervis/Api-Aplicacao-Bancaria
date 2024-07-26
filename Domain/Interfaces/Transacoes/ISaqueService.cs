using Crosscutting.Dto;

namespace Domain.Interfaces.Transacoes;

public interface ISaqueService
{
    Task<TransacaoResponseDto> RealizarSaque(SaqueRequestDto saqueRequestDto);
}
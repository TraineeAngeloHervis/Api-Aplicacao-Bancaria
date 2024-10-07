using Crosscutting.Dto;

namespace Domain.Interfaces.Transacoes;

public interface ITransferenciaService
{
    Task<TransacaoResponseDto> RealizarTransferencia(TransferenciaRequestDto transferenciaRequestDto);
}

using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface ITransacaoService
{
    Task<TransacaoResponseDto> RealizarSaque(TransacaoRequestDto transacaoRequestDto);
    Task<TransacaoResponseDto> RealizarDeposito(TransacaoRequestDto transacaoRequestDto);
    Task<TransacaoResponseDto> RealizarTransferencia(TransacaoRequestDto transacaoRequestDto);
    Task<IEnumerable<TransacaoResponseDto>> GerarExtrato(Guid contaId);
    Task<TransacaoResponseDto> ConsultarTransacao(Guid id);
}

using Crosscutting.Dto;

namespace Domain.Interfaces.Transacoes;

public interface IDepositoService
{
    Task<TransacaoResponseDto> RealizarDeposito(DepositoRequestDto depositoRequestDto);
}
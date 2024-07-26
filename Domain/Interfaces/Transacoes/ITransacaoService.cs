using Crosscutting.Dto;

namespace Domain.Interfaces.Transacoes;

public interface ITransacaoService
{
    Task<IEnumerable<TransacaoResponseDto>> GerarExtrato(Guid contaOrigemId);
    Task<TransacaoResponseDto> ConsultarTransacao(Guid id);
}
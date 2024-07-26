using AutoMapper;
using Crosscutting.Dto;
using Domain.Interfaces.Transacoes;

namespace Domain.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IMapper _mapper;
    
    public TransacaoService(ITransacaoRepository transacaoRepository, IMapper mapper)
    {
        _transacaoRepository = transacaoRepository;
        _mapper = mapper;
    }

    public async Task<TransacaoResponseDto> ConsultarTransacao(Guid id)
    {
        var transacao = await _transacaoRepository.ConsultarTransacao(id);
        return transacao == null ? null : _mapper.Map<TransacaoResponseDto>(transacao);
    }

    public async Task<IEnumerable<TransacaoResponseDto>> GerarExtrato(Guid contaOrigemId)
    {
        var transacoes = await _transacaoRepository.GerarExtrato(contaOrigemId);
        return _mapper.Map<IEnumerable<TransacaoResponseDto>>(transacoes);
    }
}
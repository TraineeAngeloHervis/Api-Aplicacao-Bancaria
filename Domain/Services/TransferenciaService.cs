using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces.Transacoes;

namespace Domain.Services;

public class TransferenciaService : ITransferenciaService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IMapper _mapper;


    public TransferenciaService(ITransacaoRepository transacaoRepository, IMapper mapper)
    {
        _transacaoRepository = transacaoRepository;
        _mapper = mapper;
    }

    public async Task<TransacaoResponseDto> RealizarTransferencia(TransferenciaRequestDto transferenciaRequestDto)
    {
        ArgumentNullException.ThrowIfNull(transferenciaRequestDto);

        var contaOrigem = await _transacaoRepository.ConsultarConta(transferenciaRequestDto.ContaOrigemId);
        var contaDestino = await _transacaoRepository.ConsultarConta(transferenciaRequestDto.ContaDestinoId);

        if (contaOrigem == null || contaDestino == null || contaOrigem.Saldo < transferenciaRequestDto.Valor)
        {
            return null;
        }

        await _transacaoRepository.AtualizarSaldo(contaOrigem.Id, -transferenciaRequestDto.Valor);
        await _transacaoRepository.AtualizarSaldo(contaDestino.Id, transferenciaRequestDto.Valor);

        var transacao = _mapper.Map<Transacao>(transferenciaRequestDto);
        var transacaoSalva = await _transacaoRepository.SalvarTransacao(transacao);
        return _mapper.Map<TransacaoResponseDto>(transacaoSalva);
    }
}
using AutoMapper;
using Crosscutting.Dto;
using Crosscutting.Enums;
using Domain.Entities;
using Domain.Interfaces;

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

    public async Task<TransacaoResponseDto> RealizarSaque(TransacaoRequestDto transacaoRequestDto)
    {
        ArgumentNullException.ThrowIfNull(transacaoRequestDto);

        var conta = await _transacaoRepository.ConsultarConta(transacaoRequestDto.ContaOrigemId);
        if (conta == null) return null;
        
        var transacao = _mapper.Map<Transacao>(transacaoRequestDto);
        
        await _transacaoRepository.AtualizarSaldo(transacaoRequestDto.ContaOrigemId, -transacaoRequestDto.Valor);
        return _mapper.Map<TransacaoResponseDto>(await _transacaoRepository.SalvarTransacao(transacao));
    }

    public async Task<TransacaoResponseDto> RealizarDeposito(TransacaoRequestDto transacaoRequestDto)
    {
        ArgumentNullException.ThrowIfNull(transacaoRequestDto);
        
        await _transacaoRepository.AtualizarSaldo(transacaoRequestDto.ContaOrigemId, transacaoRequestDto.Valor);
        var transacao = _mapper.Map<Transacao>(transacaoRequestDto);
        return _mapper.Map<TransacaoResponseDto>(await _transacaoRepository.SalvarTransacao(transacao));
        
    }

    public async Task<TransacaoResponseDto> RealizarTransferencia(TransacaoRequestDto transacaoRequestDto)
    {
        ArgumentNullException.ThrowIfNull(transacaoRequestDto);

        var contaOrigem = await _transacaoRepository.ConsultarConta(transacaoRequestDto.ContaOrigemId);
        var contaDestino = await _transacaoRepository.ConsultarConta(transacaoRequestDto.ContaDestinoId);

        if (contaOrigem == null || contaDestino == null || contaOrigem.Saldo < transacaoRequestDto.Valor)
        {
            return null;
        }

        await _transacaoRepository.AtualizarSaldo(contaOrigem.Id, -transacaoRequestDto.Valor);
        await _transacaoRepository.AtualizarSaldo(contaDestino.Id, transacaoRequestDto.Valor);

        var transacao = _mapper.Map<Transacao>(transacaoRequestDto);
        transacao.TipoTransacao = TipoTransacao.Transferencia;

        var transacaoSalva = await _transacaoRepository.SalvarTransacao(transacao);
        return _mapper.Map<TransacaoResponseDto>(transacaoSalva);
    }

    public async Task<TransacaoResponseDto> ConsultarTransacao(Guid id)
    {
        var transacao = await _transacaoRepository.ConsultarTransacao(id);
        return _mapper.Map<TransacaoResponseDto>(transacao);
    }

    public async Task<IEnumerable<TransacaoResponseDto>> GerarExtrato(Guid contaId)
    {
        var extrato = await _transacaoRepository.GerarExtrato(contaId);
        return _mapper.Map<IEnumerable<TransacaoResponseDto>>(extrato);
    }
}
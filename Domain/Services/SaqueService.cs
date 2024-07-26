using AutoMapper;
using Crosscutting.Dto;
using Crosscutting.Enums;
using Domain.Entities;
using Domain.Interfaces.Transacoes;

namespace Domain.Services;

public class SaqueService : ISaqueService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IMapper _mapper;

    public SaqueService(ITransacaoRepository transacaoRepository, IMapper mapper)
    {
        _transacaoRepository = transacaoRepository;
        _mapper = mapper;
    }

    public async Task<TransacaoResponseDto> RealizarSaque(SaqueRequestDto saqueRequestDto)
    {
        ArgumentNullException.ThrowIfNull(saqueRequestDto);

        var conta = await _transacaoRepository.ConsultarConta(saqueRequestDto.ContaOrigemId);
        if (conta == null || conta.Saldo < saqueRequestDto.Valor) return null;
        
        await _transacaoRepository.AtualizarSaldo(conta.Id, -saqueRequestDto.Valor);
        saqueRequestDto.TipoTransacao = TipoTransacao.Saque;

        var saque = _mapper.Map<Transacao>(saqueRequestDto);
        var transacaoSalva = await _transacaoRepository.SalvarTransacao(saque);

        return _mapper.Map<TransacaoResponseDto>(transacaoSalva);
    }
}
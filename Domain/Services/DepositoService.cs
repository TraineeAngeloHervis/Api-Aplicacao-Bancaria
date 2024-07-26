using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces.Transacoes;

namespace Domain.Services;

public class DepositoService : IDepositoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IMapper _mapper;

    public DepositoService(ITransacaoRepository transacaoRepository, IMapper mapper)
    {
        _transacaoRepository = transacaoRepository;
        _mapper = mapper;
    }

    public async Task<TransacaoResponseDto> RealizarDeposito(DepositoRequestDto depositoRequestDto)
    {
        ArgumentNullException.ThrowIfNull(depositoRequestDto);

        var conta = await _transacaoRepository.ConsultarConta(depositoRequestDto.ContaOrigemId);
        if (conta == null) return null;
        
        await _transacaoRepository.AtualizarSaldo(conta.Id, depositoRequestDto.Valor);

        var deposito = _mapper.Map<Deposito>(depositoRequestDto);
        var transacaoSalva = await _transacaoRepository.SalvarTransacao(deposito);

        return _mapper.Map<TransacaoResponseDto>(transacaoSalva);
    }
}
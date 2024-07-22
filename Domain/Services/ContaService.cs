using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services;

public class ContaService : IContaService
{
    private readonly IContaRepository _contaRepository;
    private readonly IMapper _mapper;
    
    public ContaService(IContaRepository contaRepository, IMapper mapper)
    {
        _contaRepository = contaRepository;
        _mapper = mapper;
    }
    
    public async Task<ContaResponseDto> CadastrarConta(ContaRequestDto contaRequestDto, Guid clienteId)
    {
        ArgumentNullException.ThrowIfNull(contaRequestDto);
        ArgumentNullException.ThrowIfNull(clienteId);
        
        var conta = _mapper.Map<Conta>(contaRequestDto);
        var contaCadastrada = await _contaRepository.CadastrarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaCadastrada);
    }
    
    public async Task<ContaResponseDto> AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto)
    {
        ArgumentNullException.ThrowIfNull(contaRequestDto);
        ArgumentNullException.ThrowIfNull(clienteId);
        
        var conta = _mapper.Map<Conta>(contaRequestDto);
        var contaAtualizada = await _contaRepository.AtualizarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaAtualizada);
    }
    
    public async Task<bool> ExcluirConta(Guid id)
    {
        var conta = await _contaRepository.ConsultarConta(id);
        ArgumentNullException.ThrowIfNull(conta);
        return await _contaRepository.ExcluirConta(id);
    }
    
    public async Task<ContaResponseDto> ConsultarConta(Guid id)
    {
        var conta = await _contaRepository.ConsultarConta(id);
        ArgumentNullException.ThrowIfNull(conta);
        return _mapper.Map<ContaResponseDto>(conta);
    }
    
    public async Task<IEnumerable<ContaResponseDto>> ListarContas(Guid clienteId)
    {
        var contas = await _contaRepository.ListarContas(clienteId);
        return _mapper.Map<IEnumerable<ContaResponseDto>>(contas);
    }
}
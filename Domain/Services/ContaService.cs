using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        var conta = _mapper.Map<Conta>(contaRequestDto);
        var contaCadastrada = await _contaRepository.CadastrarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaCadastrada);
    }
    
    public async Task<ContaResponseDto> AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id)
    {
        var conta = _mapper.Map<Conta>(contaRequestDto);
        conta.Id = id;
        var contaAtualizada = await _contaRepository.AtualizarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaAtualizada);
    }
    
    public async Task<bool> ExcluirConta(Guid id)
    {
        return await _contaRepository.ExcluirConta(id);
    }
    
    public async Task<ContaResponseDto> ConsultarConta(Guid id)
    {
        var conta = await _contaRepository.ConsultarConta(id);
        return _mapper.Map<ContaResponseDto>(conta);
    }
    
    public async Task<IEnumerable<ContaResponseDto>> ListarContas(Guid clienteId)
    {
        var contas = await _contaRepository.ListarContas(clienteId);
        return _mapper.Map<IEnumerable<ContaResponseDto>>(contas);
    }
}
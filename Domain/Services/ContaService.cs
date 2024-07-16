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
    
    public ContaResponseDto CadastrarConta(Guid clienteId, ContaRequestDto contaRequestDto)
    {
        var conta = _mapper.Map<Conta>(contaRequestDto);
        var contaCadastrada = _contaRepository.CadastrarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaCadastrada);
    }
    
    public ContaResponseDto AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id)
    {
        var conta = _mapper.Map<Conta>(contaRequestDto);
        var contaAtualizada = _contaRepository.AtualizarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaAtualizada);
    }
    
    public bool ExcluirConta(Guid id)
    {
        return _contaRepository.ExcluirConta(id);
    }
    
    public ContaResponseDto ConsultarConta(Guid id)
    {
        var conta = _contaRepository.ConsultarConta(id);
        return _mapper.Map<ContaResponseDto>(conta);
    }
    
    public IEnumerable<ContaResponseDto> ListarContas(Guid clienteId)
    {
        var contas = _contaRepository.ListarContas(clienteId);
        return _mapper.Map<IEnumerable<ContaResponseDto>>(contas);
    }
}
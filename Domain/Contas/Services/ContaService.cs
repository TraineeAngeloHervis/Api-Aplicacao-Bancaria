using AutoMapper;
using Crosscutting.Dto.Contas;
using Domain.Contas.Entities;
using Domain.Contas.Interfaces;

namespace Domain.Clientes.Services;

public class ContaService : IContaService
{
    private readonly IContaRepository _contaRepository;
    private readonly IMapper _mapper;

    public ContaService(IContaRepository contaRepository, IMapper mapper)
    {
        _contaRepository = contaRepository;
        _mapper = mapper;
    }

    public ContaResponseDto CadastrarConta(ContaRequestDto contaRequestDto)
    {
        var conta = _mapper.Map<Conta>(contaRequestDto);
        var contaCadastrada = _contaRepository.CadastrarConta(conta);
        return _mapper.Map<ContaResponseDto>(contaCadastrada);
    }

    public ContaResponseDto AtualizarConta(ContaRequestDto contaRequestDto)
    {
        var conta = _mapper.Map<Conta>(contaRequestDto);
        var contaAtualizada = _contaRepository.AtualizarConta(conta);
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

    public IEnumerable<ContaResponseDto> ListarContas()
    {
        var contas = _contaRepository.ListarContas();
        return _mapper.Map<IEnumerable<ContaResponseDto>>(contas);
    }
}
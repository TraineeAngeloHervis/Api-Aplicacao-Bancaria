using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Services;

public class ContaService : IContaService
{
    private readonly IContaRepository _contaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ContaRequestDto> _contaValidator;

    public ContaService(IContaRepository contaRepository, IClienteRepository clienteRepository, IMapper mapper,
        IValidator<ContaRequestDto> contaValidator)
    {
        _contaRepository = contaRepository;
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _contaValidator = contaValidator;
    }

    //Utilizando FluentValidation
    public ContaResponseDto CadastrarConta(Guid clienteId, ContaRequestDto contaRequestDto)
    {
        var cliente = _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new Exception("Cliente não encontrado.");
        }

        var contaEhValido = _contaValidator.Validate(contaRequestDto);
        if (!contaEhValido.IsValid)
        {
            throw new ValidationException(contaEhValido.Errors);
        }

        var conta = _mapper.Map<Conta>(contaRequestDto);
        conta.Cliente = cliente;
        var contaCadastrada = _contaRepository.CadastrarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaCadastrada);
    }

    public ContaResponseDto AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id)
    {
        var cliente = _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new Exception("Cliente não encontrado.");
        }

        var conta = _contaRepository.ConsultarConta(clienteId, id);
        if (conta == null)
        {
            throw new Exception("Conta não encontrada.");
        }

        conta = _mapper.Map(contaRequestDto, conta);
        conta.Cliente = cliente;
        var contaAtualizada = _contaRepository.AtualizarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaAtualizada);
    }

    public bool ExcluirConta(Guid clienteId, Guid id)
    {
        var cliente = _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new Exception("Cliente não encontrado.");
        }

        return _contaRepository.ExcluirConta(clienteId, id);
    }

    public ContaResponseDto ConsultarConta(Guid clienteId, Guid id)
    {
        var cliente = _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new Exception("Cliente não encontrado.");
        }

        var conta = _contaRepository.ConsultarConta(clienteId, id);
        return _mapper.Map<ContaResponseDto>(conta);
    }

    public IEnumerable<ContaResponseDto> ListarContas(Guid clienteId)
    {
        var cliente = _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new Exception("Cliente não encontrado.");
        }

        var contas = _contaRepository.ListarContas(clienteId);
        return _mapper.Map<IEnumerable<ContaResponseDto>>(contas);
    }
}
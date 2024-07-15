using AutoMapper;
using Crosscutting.Dto;
using Crosscutting.Exceptions;
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
    
    public async Task<ContaResponseDto> CadastrarConta(Guid clienteId, ContaRequestDto contaRequestDto)
    {
        var cliente = await _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ContaNaoEncontradaException();
        }

        var contaEhValido = await _contaValidator.ValidateAsync(contaRequestDto);
        if (!contaEhValido.IsValid)
        {
            throw new ValidationException(contaEhValido.Errors);
        }

        var conta = _mapper.Map<Conta>(contaRequestDto);
        conta.Cliente = cliente;
        var contaCadastrada = await _contaRepository.CadastrarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaCadastrada);
    }
    
    public async Task<ContaResponseDto> AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id)
    {
        var cliente = await _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

        var conta = await _contaRepository.ConsultarConta(clienteId, id);
        if (conta == null)
        {
            throw new ContaNaoEncontradaException();
        }

        conta = _mapper.Map(contaRequestDto, conta);
        conta.Cliente = cliente;
        var contaAtualizada = await _contaRepository.AtualizarConta(clienteId, conta);
        return _mapper.Map<ContaResponseDto>(contaAtualizada);
    }
    
    public async Task<bool> ExcluirConta(Guid clienteId, Guid id)
    {
        var cliente = await _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

        return await _contaRepository.ExcluirConta(clienteId, id);
    }
    
    public async Task<ContaResponseDto> ConsultarConta(Guid clienteId, Guid id)
    {
        var cliente = await _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

        var conta = await _contaRepository.ConsultarConta(clienteId, id);
        return _mapper.Map<ContaResponseDto>(conta);
    }
    
    public async Task<IEnumerable<ContaResponseDto>> ListarContas(Guid clienteId)
    {
        var cliente = await _clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

        var contas = await _contaRepository.ListarContas(clienteId);
        return _mapper.Map<IEnumerable<ContaResponseDto>>(contas);
    }
}
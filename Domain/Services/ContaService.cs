using AutoMapper;
using Crosscutting.Dto;
using Crosscutting.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Services;

public class ContaService(
    IContaRepository contaRepository,
    IClienteRepository clienteRepository,
    IMapper mapper,
    IValidator<ContaRequestDto> contaValidator)
    : IContaService
{
    public async Task<ContaResponseDto> CadastrarConta(Guid clienteId, ContaRequestDto contaRequestDto)
    {
        var cliente = await clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ContaNaoEncontradaException();
        }

        var contaEhValido = await contaValidator.ValidateAsync(contaRequestDto);
        if (!contaEhValido.IsValid)
        {
            throw new ValidationException(contaEhValido.Errors);
        }

        var conta = mapper.Map<Conta>(contaRequestDto);
        conta.Cliente = cliente;
        var contaCadastrada = await contaRepository.CadastrarConta(clienteId, conta);
        return mapper.Map<ContaResponseDto>(contaCadastrada);
    }
    
    public async Task<ContaResponseDto> AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id)
    {
        var cliente = await clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

        var conta = await contaRepository.ConsultarConta(clienteId, id);
        if (conta == null)
        {
            throw new ContaNaoEncontradaException();
        }

        conta = mapper.Map(contaRequestDto, conta);
        conta.Cliente = cliente;
        var contaAtualizada = await contaRepository.AtualizarConta(clienteId, conta);
        return mapper.Map<ContaResponseDto>(contaAtualizada);
    }
    
    public async Task<bool> ExcluirConta(Guid clienteId, Guid id)
    {
        var cliente = await clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

        return await contaRepository.ExcluirConta(clienteId, id);
    }
    
    public async Task<ContaResponseDto> ConsultarConta(Guid clienteId, Guid id)
    {
        var cliente = await clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

        var conta = await contaRepository.ConsultarConta(clienteId, id);
        return mapper.Map<ContaResponseDto>(conta);
    }
    
    public async Task<IEnumerable<ContaResponseDto>> ListarContas(Guid clienteId)
    {
        var cliente = await clienteRepository.ConsultarCliente(clienteId);
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

        var contas = await contaRepository.ListarContas(clienteId);
        return mapper.Map<IEnumerable<ContaResponseDto>>(contas);
    }
}
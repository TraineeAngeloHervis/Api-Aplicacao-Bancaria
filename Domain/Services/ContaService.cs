using AutoMapper;
using Crosscutting.Dto;
<<<<<<< Updated upstream
using Crosscutting.Exceptions;
=======
>>>>>>> Stashed changes
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Services;

<<<<<<< Updated upstream
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
=======
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
>>>>>>> Stashed changes
        if (!contaEhValido.IsValid)
        {
            throw new ValidationException(contaEhValido.Errors);
        }

<<<<<<< Updated upstream
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
=======
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
>>>>>>> Stashed changes
    }
}
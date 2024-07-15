using AutoMapper;
using Crosscutting.Dto;
using Crosscutting.Exceptions;
using Crosscutting.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly ClienteValidator _clienteValidator;
    
    public ClienteService(IClienteRepository clienteRepository, IMapper mapper, ClienteValidator clienteValidator)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _clienteValidator = clienteValidator;
    }
    
    public async Task<ClienteResponseDto> CadastrarCliente(ClienteRequestDto clienteRequestDto)
    {
        var clienteEhValido = await _clienteValidator.ValidateAsync(clienteRequestDto);
        if (!clienteEhValido.IsValid)
        {
            throw new ValidationException(clienteEhValido.Errors);
        }
        
        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        var clienteCadastrado = await _clienteRepository.CadastrarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteCadastrado);
    }
    
    public async Task<ClienteResponseDto> AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto)
    {
        if (await _clienteRepository.ConsultarCliente(id) == null)
        {
            throw new ClienteNaoEncontradoException();
        }
        
        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        cliente.Id = id;
        var clienteAtualizado = _clienteRepository.AtualizarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteAtualizado);
    }
    
    public async Task<bool> ExcluirCliente(Guid id)
    {
        if (await _clienteRepository.ConsultarCliente(id) == null)
        {
            throw new ClienteNaoEncontradoException();
        }
        
        return await _clienteRepository.ExcluirCliente(id);
    }
    
    public async Task<ClienteResponseDto> ConsultarCliente(Guid id)
    {
        if (await _clienteRepository.ConsultarCliente(id) == null)
        {
            throw new ClienteNaoEncontradoException();
        }
        
        var cliente = await _clienteRepository.ConsultarCliente(id);
        return _mapper.Map<ClienteResponseDto>(cliente);
    }
    
    public async Task<IEnumerable<ClienteResponseDto>> ListarClientes()
    {
        var clientes = await _clienteRepository.ListarClientes();
        return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
    }
}
using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ClienteRequestDto> _clienteValidator;
    
    public ClienteService(IClienteRepository clienteRepository, IMapper mapper, IValidator<ClienteRequestDto> clienteValidator)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _clienteValidator = clienteValidator;
    }
    
    public ClienteResponseDto CadastrarCliente(ClienteRequestDto clienteRequestDto)
    {
        var clienteEhValido = _clienteValidator.Validate(clienteRequestDto);
        if (!clienteEhValido.IsValid)
        {
            throw new ValidationException(clienteEhValido.Errors);
        }

        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        var clienteCadastrado = _clienteRepository.CadastrarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteCadastrado);
    }

    public ClienteResponseDto AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto)
    {
        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        cliente.Id = id;
        var clienteAtualizado = _clienteRepository.AtualizarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteAtualizado);
    }

    public bool ExcluirCliente(Guid id)
    {
        return _clienteRepository.ExcluirCliente(id);
    }

    public ClienteResponseDto ConsultarCliente(Guid id)
    {
        var cliente = _clienteRepository.ConsultarCliente(id);
        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public IEnumerable<ClienteResponseDto> ListarClientes()
    {
        var clientes = _clienteRepository.ListarClientes();
        return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
    }
}
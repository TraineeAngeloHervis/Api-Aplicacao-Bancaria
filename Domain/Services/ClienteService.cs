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
<<<<<<< Updated upstream

=======
    
>>>>>>> Stashed changes
    public ClienteService(IClienteRepository clienteRepository, IMapper mapper, ClienteValidator clienteValidator)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _clienteValidator = clienteValidator;
    }
<<<<<<< Updated upstream

    public async Task<ClienteResponseDto> CadastrarCliente(ClienteRequestDto clienteRequestDto)
    {
        _clienteValidator.ValidarCliente(clienteRequestDto);

        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        var clienteCadastrado = await _clienteRepository.CadastrarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteCadastrado);
    }

    public async Task<ClienteResponseDto> AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto)
    {
        var clienteExistente = await _clienteRepository.ConsultarCliente(id);
=======
    
    public ClienteResponseDto CadastrarCliente(ClienteRequestDto clienteRequestDto)
    {
        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        _clienteValidator.ValidateAndThrow(clienteRequestDto);
        var clienteCadastrado = _clienteRepository.CadastrarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteCadastrado);
    }

    public ClienteResponseDto AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto)
    {
        var clienteExistente = _clienteRepository.ConsultarCliente(id);
>>>>>>> Stashed changes
        if (clienteExistente == null)
        {
            throw new ClienteNaoEncontradoException();
        }
<<<<<<< Updated upstream

        _clienteValidator.ValidarCliente(clienteRequestDto);

        _mapper.Map(clienteRequestDto, clienteExistente);
        var clienteAtualizado = await _clienteRepository.AtualizarCliente(clienteExistente);
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
        var cliente = await _clienteRepository.ConsultarCliente(id);
=======
        _clienteValidator.ValidateAndThrow(clienteRequestDto);
        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        cliente.Id = id;
        var clienteAtualizado = _clienteRepository.AtualizarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteAtualizado);
    }

    public bool ExcluirCliente(Guid id)
    {
        var clienteExistente = _clienteRepository.ConsultarCliente(id);
        if (clienteExistente == null)
        {
            throw new ClienteNaoEncontradoException();
        }
        return _clienteRepository.ExcluirCliente(id);
    }

    public ClienteResponseDto ConsultarCliente(Guid id)
    {
        var cliente = _clienteRepository.ConsultarCliente(id);
>>>>>>> Stashed changes
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }
<<<<<<< Updated upstream

        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public async Task<IEnumerable<ClienteResponseDto>> ListarClientes()
    {
        var clientes = await _clienteRepository.ListarClientes();
=======
        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public IEnumerable<ClienteResponseDto> ListarClientes()
    {
        var clientes = _clienteRepository.ListarClientes();
>>>>>>> Stashed changes
        return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
    }
}
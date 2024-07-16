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
        _clienteValidator.ValidarCliente(clienteRequestDto);

        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        var clienteCadastrado = await _clienteRepository.CadastrarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteCadastrado);
    }

    public async Task<ClienteResponseDto> AtualizarCliente(Guid id, ClienteRequestDto clienteRequestDto)
    {
        var clienteExistente = await _clienteRepository.ConsultarCliente(id);
        if (clienteExistente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

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
        if (cliente == null)
        {
            throw new ClienteNaoEncontradoException();
        }

        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public async Task<IEnumerable<ClienteResponseDto>> ListarClientes()
    {
        var clientes = await _clienteRepository.ListarClientes();
        return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
    }
}
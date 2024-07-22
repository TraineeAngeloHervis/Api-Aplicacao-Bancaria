using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<ClienteResponseDto> CadastrarCliente(ClienteRequestDto clienteRequestDto)
    {
        ArgumentNullException.ThrowIfNull(clienteRequestDto);

        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        var clienteCadastrado = await _clienteRepository.CadastrarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteCadastrado);
    }

    public async Task<ClienteResponseDto> AtualizarCliente(ClienteRequestDto clienteRequestDto)
    {
        ArgumentNullException.ThrowIfNull(clienteRequestDto);

        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        var clienteAtualizado = await _clienteRepository.AtualizarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteAtualizado);
    }

    public async Task<bool> ExcluirCliente(Guid id)
    {
        var cliente = await _clienteRepository.ConsultarCliente(id);
        ArgumentNullException.ThrowIfNull(cliente);
        return await _clienteRepository.ExcluirCliente(id);
    }

    public async Task<ClienteResponseDto> ConsultarCliente(Guid id)
    {
        var cliente = await _clienteRepository.ConsultarCliente(id);
        ArgumentNullException.ThrowIfNull(cliente);
        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public async Task<IEnumerable<ClienteResponseDto>> ListarClientes()
    {
        var clientes = await _clienteRepository.ListarClientes();
        return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
    }
}
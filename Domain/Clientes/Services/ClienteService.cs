using Domain.Clientes.Interfaces;
using Domain.Clientes.Entities;
using Crosscutting.Dto.Clientes;
using AutoMapper;

namespace Domain.Clientes.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public ClienteResponseDto CadastrarCliente(ClienteRequestDto clienteRequestDto)
    {
        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
        var clienteCadastrado = _clienteRepository.CadastrarCliente(cliente);
        return _mapper.Map<ClienteResponseDto>(clienteCadastrado);
    }

    public ClienteResponseDto AtualizarCliente(ClienteRequestDto clienteRequestDto)
    {
        var cliente = _mapper.Map<Cliente>(clienteRequestDto);
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
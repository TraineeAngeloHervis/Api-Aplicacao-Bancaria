using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FluentAssertions;
using Moq;
using Test.Crosscutting;

namespace Test.Domain;

public class ClienteServiceTests
{
    private readonly Mock<IClienteRepository> _clienteRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly ClienteService _clienteService;

    public ClienteServiceTests()
    {
        _clienteRepository = new Mock<IClienteRepository>();
        _mapper = new Mock<IMapper>();
        _clienteService = new ClienteService(_clienteRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task Cliente_QuandoCadastrarCliente_DeveRetornarCliente()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().Build();
        var cliente = new Cliente
        {
            Nome = clienteRequestDto.Nome,
            Cpf = clienteRequestDto.Cpf,
            DataNascimento = clienteRequestDto.DataNascimento,
            EstadoCivil = clienteRequestDto.EstadoCivil
        };
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo()
            .ComNome(clienteRequestDto.Nome)
            .ComCpf(clienteRequestDto.Cpf)
            .ComDataNascimento(clienteRequestDto.DataNascimento)
            .ComEstadoCivil(clienteRequestDto.EstadoCivil)
            .Build();

        _mapper.Setup(x => x.Map<Cliente>(clienteRequestDto)).Returns(cliente);
        _clienteRepository.Setup(x => x.CadastrarCliente(cliente)).ReturnsAsync(cliente);
        _mapper.Setup(x => x.Map<ClienteResponseDto>(cliente)).Returns(clienteResponseDto);

        // Act
        var resultado = await _clienteService.CadastrarCliente(clienteRequestDto);

        // Assert
        resultado.Should().BeEquivalentTo(clienteResponseDto);
        _mapper.Verify(x => x.Map<Cliente>(clienteRequestDto), Times.Once);
        _clienteRepository.Verify(x => x.CadastrarCliente(cliente), Times.Once);
        _mapper.Verify(x => x.Map<ClienteResponseDto>(cliente), Times.Once);
    }
    
    [Fact]
    public async Task Cliente_QuandoAtualizarCliente_DeveRetornarClienteAtualizado()
    {
        // Arrange
        var id = Guid.NewGuid();
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().Build();
        var cliente = new Cliente
        {
            Id = id,
            Nome = clienteRequestDto.Nome,
            Cpf = clienteRequestDto.Cpf,
            DataNascimento = clienteRequestDto.DataNascimento,
            EstadoCivil = clienteRequestDto.EstadoCivil
        };
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo()
            .ComId(id)
            .ComNome(clienteRequestDto.Nome)
            .ComCpf(clienteRequestDto.Cpf)
            .ComDataNascimento(clienteRequestDto.DataNascimento)
            .ComEstadoCivil(clienteRequestDto.EstadoCivil)
            .Build();

        _mapper.Setup(x => x.Map<Cliente>(clienteRequestDto)).Returns(cliente);
        _clienteRepository.Setup(x => x.AtualizarCliente(cliente)).ReturnsAsync(cliente);
        _mapper.Setup(x => x.Map<ClienteResponseDto>(cliente)).Returns(clienteResponseDto);

        // Act
        var resultado = await _clienteService.AtualizarCliente(id, clienteRequestDto);

        // Assert
        resultado.Should().BeEquivalentTo(clienteResponseDto);
        _mapper.Verify(x => x.Map<Cliente>(clienteRequestDto), Times.Once);
        _clienteRepository.Verify(x => x.AtualizarCliente(cliente), Times.Once);
        _mapper.Verify(x => x.Map<ClienteResponseDto>(cliente), Times.Once);
    }
    
    [Fact]
    public async Task Cliente_QuandoExcluirCliente_DeveRetornarTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        _clienteRepository.Setup(x => x.ExcluirCliente(id)).ReturnsAsync(true);

        // Act
        var resultado = await _clienteService.ExcluirCliente(id);

        // Assert
        resultado.Should().BeTrue();
        _clienteRepository.Verify(x => x.ExcluirCliente(id), Times.Once);
    }
    
    [Fact]
    public async Task Cliente_QuandoConsultarCliente_DeveRetornarCliente()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cliente = ClienteResponseDtoBuilder.Novo().Build();
        _clienteRepository.Setup(x => x.ConsultarCliente(id)).ReturnsAsync(new Cliente());
        _mapper.Setup(x => x.Map<ClienteResponseDto>(It.IsAny<Cliente>())).Returns(cliente);
        
        // Act
        var resultado = await _clienteService.ConsultarCliente(id);
        
        // Assert
        resultado.Should().BeEquivalentTo(cliente);
        _clienteRepository.Verify(x => x.ConsultarCliente(id), Times.Once);
        _mapper.Verify(x => x.Map<ClienteResponseDto>(It.IsAny<Cliente>()), Times.Once);
    }
    
    [Fact]
    public async Task Cliente_QuandoListarClientes_DeveRetornarClientes()
    {
        // Arrange
        var clientes = new List<ClienteResponseDto>()
        {
            ClienteResponseDtoBuilder.Novo().Build(),
            ClienteResponseDtoBuilder.Novo().Build(),
            ClienteResponseDtoBuilder.Novo().Build()
        };
        _clienteRepository.Setup(x => x.ListarClientes()).ReturnsAsync(new List<Cliente>());
        _mapper.Setup(x => x.Map<IEnumerable<ClienteResponseDto>>(It.IsAny<IEnumerable<Cliente>>())).Returns(clientes);
        
        // Act
        var resultado = await _clienteService.ListarClientes();
        
        // Assert
        resultado.Should().BeEquivalentTo(clientes);
        _clienteRepository.Verify(x => x.ListarClientes(), Times.Once);
        _mapper.Verify(x => x.Map<IEnumerable<ClienteResponseDto>>(It.IsAny<IEnumerable<Cliente>>()), Times.Once);
    }
}
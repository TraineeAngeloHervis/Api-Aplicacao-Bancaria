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
        var cliente = ClienteBuilder.Novo().Build();
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo().ComClienteRequest(clienteRequestDto).Build();
        _mapper.Setup(x => x.Map<Cliente>(clienteRequestDto)).Returns(cliente);
        _clienteRepository.Setup(x => x.CadastrarCliente(cliente)).ReturnsAsync(cliente);
        _mapper.Setup(x => x.Map<ClienteResponseDto>(cliente)).Returns(clienteResponseDto);

        // Act
        var resultadoEsperado = await _clienteService.CadastrarCliente(clienteRequestDto);

        // Assert
        resultadoEsperado.Should().BeEquivalentTo(clienteResponseDto);
        _mapper.Verify(x => x.Map<Cliente>(clienteRequestDto), Times.Once);
        _clienteRepository.Verify(x => x.CadastrarCliente(cliente), Times.Once);
        _mapper.Verify(x => x.Map<ClienteResponseDto>(cliente), Times.Once);
    }
    
    [Fact]
    public async Task Cliente_QuandoAtualizarCliente_DeveRetornarClienteAtualizado()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().Build();
        var cliente = ClienteBuilder.Novo().Build();
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo().ComClienteRequest(clienteRequestDto).Build();

        _mapper.Setup(x => x.Map<Cliente>(clienteRequestDto)).Returns(cliente);
        _clienteRepository.Setup(x => x.AtualizarCliente(cliente)).ReturnsAsync(cliente);
        _mapper.Setup(x => x.Map<ClienteResponseDto>(cliente)).Returns(clienteResponseDto);

        // Act
        var resultadoEsperado = await _clienteService.AtualizarCliente(It.IsAny<Guid>(), clienteRequestDto);

        // Assert
        resultadoEsperado.Should().BeEquivalentTo(clienteResponseDto);
        _mapper.Verify(x => x.Map<Cliente>(clienteRequestDto), Times.Once);
        _clienteRepository.Verify(x => x.AtualizarCliente(cliente), Times.Once);
        _mapper.Verify(x => x.Map<ClienteResponseDto>(cliente), Times.Once);
    }
    
    [Fact]
    public async Task Cliente_QuandoExcluirCliente_DeveRetornarTrue()
    {
        // Arrange
        var cliente = ClienteBuilder.Novo().Build();
        _clienteRepository.Setup(x => x.ConsultarCliente(It.IsAny<Guid>())).ReturnsAsync(cliente);
        _clienteRepository.Setup(x => x.ExcluirCliente(It.IsAny<Guid>())).ReturnsAsync(true);
        
        // Act
        var resultadoEsperado = await _clienteService.ExcluirCliente(It.IsAny<Guid>());
        
        // Assert
        resultadoEsperado.Should().BeTrue();
        _clienteRepository.Verify(x => x.ConsultarCliente(It.IsAny<Guid>()), Times.Once);
        _clienteRepository.Verify(x => x.ExcluirCliente(It.IsAny<Guid>()), Times.Once);
    }
    
    [Fact]
    public async Task Cliente_QuandoConsultarCliente_DeveRetornarCliente()
    {
        // Arrange
        var cliente = ClienteResponseDtoBuilder.Novo().Build();
        _clienteRepository.Setup(x => x.ConsultarCliente(cliente.Id)).ReturnsAsync(new Cliente());
        _mapper.Setup(x => x.Map<ClienteResponseDto>(It.IsAny<Cliente>())).Returns(cliente);
        
        // Act
        var resultadoEsperado = await _clienteService.ConsultarCliente(cliente.Id);
        
        // Assert
        resultadoEsperado.Should().BeEquivalentTo(cliente);
        _clienteRepository.Verify(x => x.ConsultarCliente(cliente.Id), Times.Once);
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
        var resultadoEsperado = await _clienteService.ListarClientes();
        
        // Assert
        resultadoEsperado.Should().BeEquivalentTo(clientes);
        _clienteRepository.Verify(x => x.ListarClientes(), Times.Once);
        _mapper.Verify(x => x.Map<IEnumerable<ClienteResponseDto>>(It.IsAny<IEnumerable<Cliente>>()), Times.Once);
    }
}
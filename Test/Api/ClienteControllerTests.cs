using Api.Controllers;
using Bogus;
using Crosscutting.Dto;
using Domain.Interfaces;
using Moq;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Test.Builders;

namespace Test.Api;

public class ClienteControllerTests
{
    private readonly Mock<IClienteService> _clienteServiceMock;
    private readonly ClienteController _clienteController;
    
    public ClienteControllerTests()
    {
        _clienteServiceMock = new Mock<IClienteService>();
        _clienteController = new ClienteController(_clienteServiceMock.Object);
    }

    [Fact]
    public void Cliente_QuandoCadastrarCliente_DeveRetornarCliente()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().Build();
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo().Build();
        _clienteServiceMock.Setup(x => x.CadastrarCliente(clienteRequestDto)).Returns(clienteResponseDto);
        
        // Act
        var resultadoEsperado = _clienteController.CadastrarCliente(clienteRequestDto) as CreatedAtActionResult;
        
        // Assert
        resultadoEsperado.Should().NotBeNull();
        resultadoEsperado!.ActionName.Should().Be(nameof(ClienteController.ConsultarCliente));
    }
    
    [Fact]
    public void Cliente_QuandoAtualizarCliente_DeveRetornarClienteAtualizado()
    {
        // Arrange
        var clienteNovo = ClienteResponseDtoBuilder.Novo().ComNome("Cliente Novo").Build();
        var clienteAtualizado = ClienteResponseDtoBuilder.Novo().ComNome("Cliente Atualizado").Build();
        _clienteServiceMock.Setup(x => x.AtualizarCliente(clienteNovo.Id, It.IsAny<ClienteRequestDto>())).Returns(clienteAtualizado);
        
        // Act
        var resultadoEsperado = _clienteController.AtualizarCliente(clienteNovo.Id, new ClienteRequestDto()) as OkObjectResult;
        
        // Assert
        resultadoEsperado.Should().NotBeNull();
        resultadoEsperado!.Value.Should().Be(clienteAtualizado);
    }
    
    [Fact]
    public void Cliente_QuandoExcluirCliente_DeveRetornarNoContent()
    {
        // Arrange
        var cliente = ClienteResponseDtoBuilder.Novo().Build();
        _clienteServiceMock.Setup(x => x.ExcluirCliente(cliente.Id)).Returns(true);
        
        // Act
        var resultadoEsperado = _clienteController.ExcluirCliente(cliente.Id) as NoContentResult;
        
        // Assert
        resultadoEsperado.Should().NotBeNull();
    }
    
    [Fact]
    public void Cliente_QuandoConsultarCliente_DeveRetornarCliente()
    {
        // Arrange
        var cliente = ClienteResponseDtoBuilder.Novo().Build();
        _clienteServiceMock.Setup(x => x.ConsultarCliente(cliente.Id)).Returns(cliente);
        
        // Act
        var resultadoEsperado = _clienteController.ConsultarCliente(cliente.Id) as OkObjectResult;
        
        // Assert
        resultadoEsperado.Should().NotBeNull();
        resultadoEsperado!.Value.Should().Be(cliente);
    }
    
    [Fact]
    public void Cliente_QuandoConsultarClienteInexistente_DeveRetornarNotFound()
    {
        // Arrange
        var cliente = ClienteResponseDtoBuilder.Novo().Build();
        _clienteServiceMock.Setup(x => x.ConsultarCliente(cliente.Id)).Returns((ClienteResponseDto)null);
        
        // Act
        var resultadoEsperado = _clienteController.ConsultarCliente(cliente.Id) as NotFoundResult;
        
        // Assert
        resultadoEsperado.Should().NotBeNull();
    }
    
    [Fact]
    public void Cliente_QuandoListarClientes_DeveRetornarClientes()
    {
        // Arrange
        var clientes = new Faker<ClienteResponseDto>("pt_BR").Generate(10);
        _clienteServiceMock.Setup(x => x.ListarClientes()).Returns(clientes);
        
        // Act
        var resultadoEsperado = _clienteController.ListarClientes() as OkObjectResult;
        
        // Assert
        resultadoEsperado.Should().NotBeNull();
        resultadoEsperado!.Value.Should().Be(clientes);
    }
    
    [Fact]
    public void Cliente_QuandoListarClientesVazio_DeveRetornarListaVazia()
    {
        // Arrange
        _clienteServiceMock.Setup(x => x.ListarClientes()).Returns(new List<ClienteResponseDto>());
        
        // Act
        var resultadoEsperado = _clienteController.ListarClientes() as OkObjectResult;
        
        // Assert
        resultadoEsperado.Should().NotBeNull();
        resultadoEsperado!.Value.As<IEnumerable<ClienteResponseDto>>().Should().BeEmpty();
    }
    
    [Fact]
    public void Cliente_QuandoListarClientesNulo_DeveRetornarNotFound()
    {
        // Arrange
        _clienteServiceMock.Setup(x => x.ListarClientes()).Returns((List<ClienteResponseDto>)null);
        
        // Act
        var resultadoEsperado = _clienteController.ListarClientes() as NotFoundResult;
        
        // Assert
        resultadoEsperado.Should().NotBeNull();
    }
}
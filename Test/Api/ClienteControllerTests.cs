using Api.Controllers;
using Crosscutting.Dto;
using Domain.Interfaces;
using Domain.Interfaces.Cliente;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.Crosscutting;

namespace Test.Api;

public class ClienteControllerTests
{
    private readonly Mock<IClienteService> _clienteServiceMock;
    private readonly Mock<IClienteValidator> _clienteValidatorMock;
    private readonly ClienteController _clienteController;


    public ClienteControllerTests()
    {
        _clienteServiceMock = new Mock<IClienteService>();
        _clienteValidatorMock = new Mock<IClienteValidator>();
        _clienteController = new ClienteController(_clienteServiceMock.Object, _clienteValidatorMock.Object);
    }

    [Fact]
    public async Task Cliente_QuandoCadastrarCliente_DeveRetornarCliente()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().Build();
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo().Build();
        
        _clienteValidatorMock.Setup(v => v.EhValido(It
                .IsAny<ClienteRequestDto>(), out It.Ref<IList<string>>.IsAny))
            .ReturnsAsync(true);

        _clienteServiceMock.Setup(x => x.CadastrarCliente(It
                .IsAny<ClienteRequestDto>()))
            .ReturnsAsync(clienteResponseDto);

        // Act
        var resultadoEsperado = await _clienteController.CadastrarCliente(clienteRequestDto);

        // Assert
        resultadoEsperado.Should().BeOfType<CreatedAtActionResult>();
        var createdAtActionResult = (CreatedAtActionResult)resultadoEsperado;
        createdAtActionResult.Value.Should().Be(clienteResponseDto);
    }

    [Fact]
    public async Task Cliente_QuandoAtualizarCliente_DeveRetornarClienteAtualizado()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().Build();
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo().Build();

        _clienteValidatorMock.Setup(v => v.EhValido(It
                .IsAny<ClienteRequestDto>(), out It.Ref<IList<string>>.IsAny))
            .ReturnsAsync(true);
        
        _clienteServiceMock.Setup(x => x.AtualizarCliente(It.IsAny<Guid>(), It
                .IsAny<ClienteRequestDto>()))
            .ReturnsAsync(clienteResponseDto);

        // Act
        var resultadoEsperado = await _clienteController
            .AtualizarCliente(clienteResponseDto.Id, clienteRequestDto);

        // Assert
        resultadoEsperado.Should().BeOfType<OkObjectResult>();
        var okObjectResult = (OkObjectResult)resultadoEsperado;
        okObjectResult.Value.Should().BeEquivalentTo(clienteResponseDto);
    }


    [Fact]
    public async Task Cliente_QuandoExcluirCliente_DeveRetornarNoContent()
    {
        // Arrange
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo().Build();
        _clienteServiceMock.Setup(x => x.ExcluirCliente(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        
        // Act
        var resultadoEsperado = await _clienteController.ExcluirCliente(clienteResponseDto.Id);
        
        // Assert
        resultadoEsperado.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Cliente_QuandoConsultarCliente_DeveRetornarCliente()
    {
        // Arrange
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo().Build();
        _clienteServiceMock.Setup(x => x.ConsultarCliente(It.IsAny<Guid>()))
            .ReturnsAsync(clienteResponseDto);

        // Act
        var resultadoEsperado = await _clienteController.ConsultarCliente(clienteResponseDto.Id);
        var okObjectResult = (OkObjectResult)resultadoEsperado;
        var clienteRetornado = okObjectResult.Value as ClienteResponseDto;

        // Assert
        resultadoEsperado.Should().BeOfType<OkObjectResult>();
        clienteRetornado.Should().NotBeNull();
        clienteRetornado.Should().BeEquivalentTo(clienteResponseDto);
    }

    [Fact]
    public async Task Cliente_QuandoListarClientes_DeveRetornarClientes()
    {
        // Arrange
        var clientes = new List<ClienteResponseDto>
        {
            ClienteResponseDtoBuilder.Novo().Build(),
            ClienteResponseDtoBuilder.Novo().Build(),
            ClienteResponseDtoBuilder.Novo().Build()
        };

        _clienteServiceMock.Setup(x => x.ListarClientes())
            .ReturnsAsync(clientes);

        // Act
        var resultadoEsperado = await _clienteController.ListarClientes();
        var okObjectResult = (OkObjectResult)resultadoEsperado;
        var clientesRetornados = okObjectResult.Value as List<ClienteResponseDto>;

        // Assert
        resultadoEsperado.Should().BeOfType<OkObjectResult>();
        clientesRetornados.Should().NotBeNull();
        clientesRetornados.Should().HaveCount(3);
    }
}
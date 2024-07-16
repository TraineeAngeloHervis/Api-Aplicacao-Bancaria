using Api.Controllers;
using Bogus;
using Crosscutting.Dto;
using Domain.Interfaces;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Test.Builders;

namespace Test.Api;

public class ContaControllerTests
{
    private readonly Mock<IContaService> _contaServiceMock;
    private readonly ContaController _contaController;
    private readonly Mock<IClienteService> _clienteServiceMock;
    private readonly ClienteController _clienteController;

    [Fact]
    public void Conta_QuandoCadastrarConta_DeveRetornarConta()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().Build();
        var clienteResponseDto = ClienteResponseDtoBuilder.Novo().Build();
        var contaRequestDto = ContaRequestDtoBuilder.Novo().Build();
        var contaResponseDto = ContaResponseDtoBuilder.Novo().Build();
        _clienteServiceMock.Setup(x => x.CadastrarCliente(It.IsAny<ClienteRequestDto>())).Returns(clienteResponseDto);
        
        _contaServiceMock.Setup(x => x.CadastrarConta(It.IsAny<Guid>(), It.IsAny<ContaRequestDto>())).Returns(contaResponseDto);
        
        // Act
        var resultadoEsperado = _contaController.CadastrarConta(clienteResponseDto.Id, contaRequestDto) as CreatedAtActionResult;
        
        // Assert
        resultadoEsperado.Should().NotBeNull();
        resultadoEsperado.ActionName.Should().Be(nameof(_contaController.ConsultarConta));
        resultadoEsperado.RouteValues.Should().NotBeNull();
        resultadoEsperado.RouteValues["clienteId"].Should().Be(clienteResponseDto.Id);
        resultadoEsperado.RouteValues["id"].Should().Be(contaResponseDto.Id);
        resultadoEsperado.Value.Should().Be(contaResponseDto);
    }
}
using Api.Controllers;
using Crosscutting.Dto;
using Domain.Interfaces;
using Domain.Validators;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.Crosscutting;

namespace Test.Api;

public class ContaControllerTests
{
    private readonly Mock<IContaService> _contaService;
    private readonly Mock<IClienteService> _clienteService;
    private readonly Mock<IContaValidator> _contaValidator;
    private readonly ContaController _contaController;

    public ContaControllerTests()
    {
        _contaService = new Mock<IContaService>();
        _clienteService = new Mock<IClienteService>();
        _contaValidator = new Mock<IContaValidator>();
        _contaController = new ContaController(_contaService.Object, _clienteService.Object, new ContaValidator());
    }

    [Fact]
    public async Task Conta_QuandoCadastrarConta_DeveRetornarConta()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().Build();
        var contaResponseDto = ContaResponseDtoBuilder.Novo().Build();

        _contaValidator.Setup(v => v.EhValido(It
                .IsAny<ContaRequestDto>(), out It.Ref<IList<string>>.IsAny))
            .Returns(true);

        _clienteService.Setup(x => x.ConsultarCliente(It
                .IsAny<Guid>()))
            .ReturnsAsync(ClienteResponseDtoBuilder.Novo().Build());

        _contaService.Setup(x => x.CadastrarConta(It
                .IsAny<ContaRequestDto>(), It.IsAny<Guid>()))
            .ReturnsAsync(contaResponseDto);

        // Act
        var resultadoEsperado = await _contaController.CadastrarConta(contaRequestDto);

        // Assert
        resultadoEsperado.Should().BeOfType<CreatedAtActionResult>();
        var createdAtActionResult = (CreatedAtActionResult)resultadoEsperado;
        createdAtActionResult.Value.Should().Be(contaResponseDto);
    }

    [Fact]
    public async Task Conta_QuandoAtualizarConta_DeveRetornarContaAtualizada()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().Build();
        var contaResponseDto = ContaResponseDtoBuilder.Novo().Build();

        _contaValidator.Setup(v => v.EhValido(It
                .IsAny<ContaRequestDto>(), out It.Ref<IList<string>>.IsAny))
            .Returns(true);

        _clienteService.Setup(x => x.ConsultarCliente(It
                .IsAny<Guid>()))
            .ReturnsAsync(ClienteResponseDtoBuilder.Novo().Build());
        
        _contaService.Setup(x => x.AtualizarConta(It
                .IsAny<Guid>(), It.IsAny<ContaRequestDto>(), It.IsAny<Guid>()))
            .ReturnsAsync(contaResponseDto);

        // Act
        var resultadoEsperado = await _contaController.AtualizarConta(contaResponseDto.Id, contaRequestDto);

        // Assert
        resultadoEsperado.Should().BeOfType<OkObjectResult>();
        var okObjectResult = (OkObjectResult)resultadoEsperado;
        okObjectResult.Value.Should().Be(contaResponseDto);
    }

    [Fact]
    public async Task Conta_QuandoExcluirConta_DeveRetornarNotFound()
    {
        // Arrange
        var contaResponseDto = ContaResponseDtoBuilder.Novo().Build();
        _contaService.Setup(x => x.ExcluirConta(It
                .IsAny<Guid>()))
            .ReturnsAsync(true);

        // Act
        var resultadoEsperado = await _contaController.ExcluirConta(contaResponseDto.Id);

        // Assert
        resultadoEsperado.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Conta_QuandoConsultarConta_DeveRetornarConta()
    {
        // Arrange
        var contaResponseDto = ContaResponseDtoBuilder.Novo().Build();
        _contaService.Setup(x => x.ConsultarConta(It
                .IsAny<Guid>()))
            .ReturnsAsync(contaResponseDto);

        // Act
        var resultadoEsperado = await _contaController.ConsultarConta(contaResponseDto.Id);

        // Assert
        resultadoEsperado.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task Conta_QuandoListarContas_DeveRetornarContas()
    {
        // Arrange
        var contasResponseDto = new List<ContaResponseDto>()
        {
            ContaResponseDtoBuilder.Novo().Build(),
            ContaResponseDtoBuilder.Novo().Build(),
            ContaResponseDtoBuilder.Novo().Build()
        };
        
        _contaService.Setup(x => x.ListarContas(It
                .IsAny<Guid>()))
            .ReturnsAsync(contasResponseDto);
        
        // Act
        var resultadoEsperado = await _contaController.ListarContas(It.IsAny<Guid>());
        var okObjectResult = (OkObjectResult)resultadoEsperado;
        var contasRetornadas = okObjectResult.Value as List<ContaResponseDto>;
        
        // Assert
        resultadoEsperado.Should().BeOfType<OkObjectResult>();
        contasRetornadas.Should().NotBeNull();
        contasRetornadas.Should().HaveCount(3);
        
    }
}
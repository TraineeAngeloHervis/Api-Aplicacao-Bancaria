using Domain.Validators;
using FluentAssertions;
using Test.Crosscutting.Transacoes;

namespace Test.Domain.Validators;

public class TranferenciaValidatorTests
{
    private readonly TransferenciaValidator _transferenciaValidator = new();
    
    [Fact]
    public async Task Transferencia_QuandoTransferenciaValida_DeveRetornarTrue()
    {
        // Arrange
        var transferenciaRequestDto = TransferenciaRequestDtoBuilder.Novo().Build();

        // Act
        var resultadoEsperado = await _transferenciaValidator.EhValido(transferenciaRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(true);
        errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Transferencia_QuandoTransferenciaComContaOrigemIdVazio_DeveRetornarErroContaOrigemIdObrigatorio()
    {
        // Arrange
        var transferenciaRequestDto = TransferenciaRequestDtoBuilder.Novo().ComContaOrigemId(Guid.Empty).Build();

        // Act
        const string erroEsperado = "O campo ContaOrigemId é obrigatório.";
        var resultadoEsperado = await _transferenciaValidator.EhValido(transferenciaRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }
    
    [Fact]
    public async Task Transferencia_QuandoTransferenciaComContaDestinoIdVazio_DeveRetornarErroContaDestinoIdObrigatorio()
    {
        // Arrange
        var transferenciaRequestDto = TransferenciaRequestDtoBuilder.Novo().ComContaDestinoId(Guid.Empty).Build();

        // Act
        const string erroEsperado = "O campo ContaDestinoId é obrigatório.";
        var resultadoEsperado = await _transferenciaValidator.EhValido(transferenciaRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }
    
    [Fact]
    public async Task Transferencia_QuandoTransferenciaComValorNegativo_DeveRetornarErroValorNegativo()
    {
        // Arrange
        var transferenciaRequestDto = TransferenciaRequestDtoBuilder.Novo().ComValor(-1).Build();

        // Act
        const string erroEsperado = "O valor da transferência deve ser positivo.";
        var resultadoEsperado = await _transferenciaValidator.EhValido(transferenciaRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }
}
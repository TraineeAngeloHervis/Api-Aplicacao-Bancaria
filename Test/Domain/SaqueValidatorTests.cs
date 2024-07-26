using Crosscutting.Enums;
using Domain.Validators;
using FluentAssertions;
using Test.Crosscutting;

namespace Test.Domain;

public class SaqueValidatorTests
{
    private readonly SaqueValidator _saqueValidator = new();

    [Fact]
    public async Task Saque_QuandoSaqueValido_DeveRetornarTrue()
    {
        // Arrange
        var transacaoRequestDto = TransacaoRequestDtoBuilder.Novo().Build();

        // Act
        var resultadoEsperado = await _saqueValidator.EhValido(transacaoRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(true);
        errors.Should().BeEmpty();
    }

    [Fact]
    public async Task Saque_QuandoSaqueComContaOrigemIdVazio_DeveRetornarErroContaOrigemIdObrigatorio()
    {
        // Arrange
        var transacaoRequestDto = TransacaoRequestDtoBuilder.Novo().ComContaOrigemId(Guid.Empty).Build();

        // Act
        const string erroEsperado = "O campo ContaOrigemId é obrigatório.";
        var resultadoEsperado = await _saqueValidator.EhValido(transacaoRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task Saque_QuandoSaqueComValorNegativo_DeveRetornarErroValorNegativo()
    {
        // Arrange
        var transacaoRequestDto = TransacaoRequestDtoBuilder.Novo().ComValor(-1).Build();

        // Act
        const string erroEsperado = "O valor do saque deve ser positivo.";
        var resultadoEsperado = await _saqueValidator.EhValido(transacaoRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task Saque_QuandoSaqueComTipoTransacaoDeposito_DeveRetornarErroTipoTransacaoInvalido()
    {
        // Arrange
        var transacaoRequestDto = TransacaoRequestDtoBuilder.Novo().ComTipoTransacao(TipoTransacao.Deposito).Build();

        // Act
        const string erroEsperado = "Tipo de transação inválido para saque.";
        var resultadoEsperado = await _saqueValidator.EhValido(transacaoRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task
        Saque_QuandoSaqueComContaOrigemIdDiferenteDeContaDestinoId_DeveRetornarErroContaOrigemDestinoDiferentes()
    {
        // Arrange
        var transacaoRequestDto = TransacaoRequestDtoBuilder.Novo().ComContaOrigemId(Guid.NewGuid())
            .ComContaDestinoId(Guid.NewGuid()).Build();

        // Act
        const string erroEsperado = "Contas de origem e conta de destino devem ser iguais.";
        var resultadoEsperado = await _saqueValidator.EhValido(transacaoRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }
}
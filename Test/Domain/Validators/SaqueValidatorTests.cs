using Domain.Validators;
using FluentAssertions;
using Test.Crosscutting.Transacoes;

namespace Test.Domain.Validators;

public class SaqueValidatorTests
{
    private readonly SaqueValidator _saqueValidator = new();

    [Fact]
    public async Task Saque_QuandoSaqueValido_DeveRetornarTrue()
    {
        // Arrange
        var saqueRequestDto = SaqueRequestDtoBuilder.Novo().Build();

        // Act
        var resultadoEsperado = await _saqueValidator.EhValido(saqueRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(true);
        errors.Should().BeEmpty();
    }

    [Fact]
    public async Task Saque_QuandoSaqueComContaOrigemIdVazio_DeveRetornarErroContaOrigemIdObrigatorio()
    {
        // Arrange
        var saqueRequestDto = SaqueRequestDtoBuilder.Novo().ComContaOrigemId(Guid.Empty).Build();

        // Act
        const string erroEsperado = "O campo ContaOrigemId é obrigatório.";
        var resultadoEsperado = await _saqueValidator.EhValido(saqueRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task Saque_QuandoSaqueComValorNegativo_DeveRetornarErroValorNegativo()
    {
        // Arrange
        var saqueRequestDto = SaqueRequestDtoBuilder.Novo().ComValor(-1).Build();

        // Act
        const string erroEsperado = "O valor do saque deve ser maior que zero.";
        var resultadoEsperado = await _saqueValidator.EhValido(saqueRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }
}
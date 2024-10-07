using Domain.Validators;
using FluentAssertions;
using Test.Crosscutting.Transacoes;

namespace Test.Domain.Validators;

public class DepositoValidatorTests
{
    private readonly DepositoValidator _depositoValidator = new();
    
    [Fact]
    public async Task Deposito_QuandoDepositoValido_DeveRetornarTrue()
    {
        // Arrange
        var depositoRequestDto = DepositoRequestDtoBuilder.Novo().Build();

        // Act
        var resultadoEsperado = await _depositoValidator.EhValido(depositoRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(true);
        errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Deposito_QuandoDepositoComContaOrigemIdVazio_DeveRetornarErroContaOrigemIdObrigatorio()
    {
        // Arrange
        var depositoRequestDto = DepositoRequestDtoBuilder.Novo().ComContaOrigemId(Guid.Empty).Build();

        // Act
        const string erroEsperado = "O campo ContaOrigemId é obrigatório.";
        var resultadoEsperado = await _depositoValidator.EhValido(depositoRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }
    
    [Fact]
    public async Task Deposito_QuandoDepositoComValorNegativo_DeveRetornarErroValorNegativo()
    {
        // Arrange
        var depositoRequestDto = DepositoRequestDtoBuilder.Novo().ComValor(-1).Build();

        // Act
        const string erroEsperado = "O valor do depósito deve ser maior que zero.";
        var resultadoEsperado = await _depositoValidator.EhValido(depositoRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }
}
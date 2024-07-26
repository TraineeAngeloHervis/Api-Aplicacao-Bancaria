using Crosscutting.Enums;
using Domain.Validators;
using FluentAssertions;
using Test.Crosscutting.Contas;

namespace Test.Domain.Validators;

public class ContaValidatorTests
{
    private readonly ContaValidator _contaValidator = new();

    [Fact]
    public async Task Conta_QuandoContaValida_DeveRetornarTrue()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().Build();

        // Act
        var resultadoEsperado = await _contaValidator.EhValido(contaRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(true);
        errors.Should().BeEmpty();
    }

    [Fact]
    public async Task Conta_QuandoContaComClienteIdVazio_DeveRetornarErroClienteIdObrigatorio()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().ComClienteId(Guid.Empty).Build();

        // Act
        const string erroEsperado = "O campo ClienteId é obrigatório.";
        var resultadoEsperado = await _contaValidator.EhValido(contaRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task Conta_QuandoContaComSaldoInicialNegativo_DeveRetornarErroSaldoInicialNegativo()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().ComSaldoInicial(-1).Build();

        // Act
        const string erroEsperado = "O saldo inicial não pode ser negativo.";
        var resultadoEsperado = await _contaValidator.EhValido(contaRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task Conta_QuandoContaComDataAberturaMaiorQueDataAtual_DeveRetornarErroDataAberturaMaiorQueDataAtual()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().ComDataAbertura(DateTime.Now.AddDays(1)).Build();

        // Act
        const string erroEsperado = "A data de abertura da conta não pode ser maior que a data atual.";
        var resultadoEsperado = await _contaValidator.EhValido(contaRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task Conta_QuandoContaComTipoContaInvalido_DeveRetornarErroTipoContaInvalido()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().ComTipoConta((TipoConta)20).Build();

        // Act
        const string erroEsperado = "Tipo de conta inválido.";
        var resultadoEsperado = await _contaValidator.EhValido(contaRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        ;
        errors.Should().Contain(erroEsperado);
    }
}
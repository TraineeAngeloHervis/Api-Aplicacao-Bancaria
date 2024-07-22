using Crosscutting.Enums;
using Domain.Validators;
using FluentAssertions;
using Test.Crosscutting;

namespace Test.Domain;

public class ContaValidatorTests
{
    private readonly ContaValidator _contaValidator = new();
    
    [Fact]
    public void Conta_QuandoContaValida_DeveRetornarTrue()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().Build();

        // Act
        var resultadoEsperado = _contaValidator.EhValido(contaRequestDto, out var errors);
        
        // Assert
        resultadoEsperado.Should().BeTrue();
        errors.Should().BeEmpty();
    }
    
    [Fact]
    public void Conta_QuandoContaComClienteIdVazio_DeveRetornarErroClienteIdObrigatorio()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().ComClienteId(Guid.Empty).Build();

        // Act
        const string erroEsperado = "O campo ClienteId é obrigatório.";
        var resultado = _contaValidator.EhValido(contaRequestDto, out var errors);
        
        // Assert
        resultado.Should().BeFalse();
        errors.Should().Contain(erroEsperado);
    }
    
    [Fact]
    public void Conta_QuandoContaComSaldoInicialNegativo_DeveRetornarErroSaldoInicialNegativo()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().ComSaldoInicial(-1).Build();

        // Act
        const string erroEsperado = "O saldo inicial não pode ser negativo.";
        var resultado = _contaValidator.EhValido(contaRequestDto, out var errors);
        
        // Assert
        resultado.Should().BeFalse();
        errors.Should().Contain(erroEsperado);
    }
    
    [Fact]
    public void Conta_QuandoContaComDataAberturaMaiorQueDataAtual_DeveRetornarErroDataAberturaMaiorQueDataAtual()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().ComDataAbertura(DateTime.Now.AddDays(1)).Build();

        // Act
        const string erroEsperado = "A data de abertura da conta não pode ser maior que a data atual.";
        var resultado = _contaValidator.EhValido(contaRequestDto, out var errors);
        
        // Assert
        resultado.Should().BeFalse();
        errors.Should().Contain(erroEsperado);
    }
    
    [Fact]
    public void Conta_QuandoContaComTipoContaInvalido_DeveRetornarErroTipoContaInvalido()
    {
        // Arrange
        var contaRequestDto = ContaRequestDtoBuilder.Novo().ComTipoConta((TipoConta) 20).Build();

        // Act
        const string erroEsperado = "Tipo de conta inválido.";
        var resultado = _contaValidator.EhValido(contaRequestDto, out var errors);
        
        // Assert
        resultado.Should().BeFalse();
        errors.Should().Contain(erroEsperado);
    }
}
using Crosscutting.Enums;
using Domain.Validators;
using FluentAssertions;
using Test.Crosscutting;

namespace Test.Domain;

public class ClienteValidatorTests
{
    private readonly ClienteValidator _clienteValidator = new();
    
    [Fact]
    public void Cliente_QuandoClienteValido_DeveRetornarTrue()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().Build();

        // Act
        var resultadoEsperado = _clienteValidator.EhValido(clienteRequestDto, out var errors);
        
        // Assert
        resultadoEsperado.Should().BeTrue();
        errors.Should().BeEmpty();
    }

    [Fact]
    public void Cliente_QuandoClienteComNomeVazio_DeveRetornarErroNomeObrigatorio()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().ComNome("").Build();

        // Act
        const string erroEsperado = "O nome do cliente é obrigatório.";
        var resultado = _clienteValidator.EhValido(clienteRequestDto, out var errors);
        
        // Assert
        resultado.Should().BeFalse();
        errors.Should().Contain(erroEsperado);
    }
    
    [Fact]
    public void Cliente_QuandoClienteComCpfInvalido_DeveRetornarErroCpfInvalido()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().ComCpf("1234567890").Build();

        // Act
        const string erroEsperado = "O CPF do cliente é inválido.";
        var resultado = _clienteValidator.EhValido(clienteRequestDto, out var errors);
        
        // Assert
        resultado.Should().BeFalse();
        errors.Should().Contain(erroEsperado);
    }
    
    [Fact]
    public void Cliente_QuandoClienteComDataNascimentoMaiorQueDataAtual_DeveRetornarErroDataNascimentoMaiorQueDataAtual()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().ComDataNascimento(DateTime.Now.AddDays(1)).Build();

        // Act
        const string erroEsperado = "A data de nascimento não pode ser maior que a data atual.";
        var resultado = _clienteValidator.EhValido(clienteRequestDto, out var errors);
        
        // Assert
        resultado.Should().BeFalse();
        errors.Should().Contain(erroEsperado);
    }
    
    [Fact]
    public void Cliente_QuandoClienteComEstadoCivilInvalido_DeveRetornarErroEstadoCivilInvalido()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().ComEstadoCivil((EstadoCivil) 20).Build();

        // Act
        const string erroEsperado = "Estado civil inválido.";
        var resultado = _clienteValidator.EhValido(clienteRequestDto, out var errors);
        
        // Assert
        resultado.Should().BeFalse();
        errors.Should().Contain(erroEsperado);
    }
}
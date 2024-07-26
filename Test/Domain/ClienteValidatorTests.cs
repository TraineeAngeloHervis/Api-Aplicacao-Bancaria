using Crosscutting.Enums;
using Domain.Validators;
using FluentAssertions;
using Test.Crosscutting;

namespace Test.Domain;

public class ClienteValidatorTests
{
    private readonly ClienteValidator _clienteValidator = new();

    [Fact]
    public async Task Cliente_QuandoClienteValido_DeveRetornarTrue()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().Build();

        // Act
        var resultadoEsperado = await _clienteValidator.EhValido(clienteRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(true);
        errors.Should().BeEmpty();
    }

    [Fact]
    public async Task Cliente_QuandoClienteComNomeVazio_DeveRetornarErroNomeObrigatorio()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().ComNome("").Build();

        // Act
        const string erroEsperado = "O nome do cliente é obrigatório.";
        var resultadoEsperado = await _clienteValidator.EhValido(clienteRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task Cliente_QuandoClienteComCpfInvalido_DeveRetornarErroCpfInvalido()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().ComCpf("1234567890").Build();

        // Act
        const string erroEsperado = "O CPF do cliente é inválido.";
        var resultadoEsperado = await _clienteValidator.EhValido(clienteRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task Cliente_QuandoClienteComDataNascimentoMaiorQueDataAtual_DeveRetornarErroMaiorQueDataAtual()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().ComDataNascimento(DateTime.Now.AddDays(1)).Build();

        // Act
        const string erroEsperado = "A data de nascimento não pode ser maior que a data atual.";
        var resultadoEsperado = await _clienteValidator.EhValido(clienteRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }

    [Fact]
    public async Task Cliente_QuandoClienteComEstadoCivilInvalido_DeveRetornarErroEstadoCivilInvalido()
    {
        // Arrange
        var clienteRequestDto = ClienteRequestDtoBuilder.Novo().ComEstadoCivil((EstadoCivil)20).Build();

        // Act
        const string erroEsperado = "Estado civil inválido.";
        var resultadoEsperado = await _clienteValidator.EhValido(clienteRequestDto, out var errors);

        // Assert
        resultadoEsperado.Should().Be(false);
        errors.Should().Contain(erroEsperado);
    }
}
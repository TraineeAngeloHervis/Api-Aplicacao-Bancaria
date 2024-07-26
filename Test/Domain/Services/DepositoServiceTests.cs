using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces.Transacoes;
using Domain.Services;
using FluentAssertions;
using Moq;
using Test.Crosscutting.Contas;
using Test.Crosscutting.Transacoes;

namespace Test.Domain.Services;

public class DepositoServiceTests
{
    private readonly Mock<ITransacaoRepository> _transacaoRepositoryMock;
    private readonly DepositoService _depositoService;
    private readonly Mock<IMapper> _mapperMock;

    public DepositoServiceTests()
    {
        _transacaoRepositoryMock = new Mock<ITransacaoRepository>();
        _mapperMock = new Mock<IMapper>();
        _depositoService = new DepositoService(_transacaoRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Deposito_QuandoRealizarDeposito_DeveRetornarTransacao()
    {
        // Arrange
        var conta = ContaBuilder.Novo().Build();
        var depositoRequestDto = DepositoRequestDtoBuilder.Novo().ComContaOrigemId(conta.Id).Build();
        var transacao = TransacaoBuilder.Novo().ComDepositoRequest(depositoRequestDto).Build();
        var transacaoResponseDto = TransacaoResponseDtoBuilder.Novo().ComTransacao(transacao).Build();

        _transacaoRepositoryMock.Setup(x => x
            .ConsultarConta(depositoRequestDto.ContaOrigemId)).ReturnsAsync(conta);
        
        _transacaoRepositoryMock.Setup(x => x
            .AtualizarSaldo(conta.Id, depositoRequestDto.Valor)).ReturnsAsync(conta);
        
        _mapperMock.Setup(x => x
            .Map<Transacao>(depositoRequestDto)).Returns(transacao);
        
        _transacaoRepositoryMock.Setup(x => x
            .SalvarTransacao(transacao)).ReturnsAsync(transacao);
        
        _mapperMock.Setup(x => x.Map<TransacaoResponseDto>(transacao)).Returns(transacaoResponseDto);

        // Act
        var resultadoEsperado = await _depositoService.RealizarDeposito(depositoRequestDto);

        // Assert
        resultadoEsperado.Should().BeEquivalentTo(transacaoResponseDto);
        _transacaoRepositoryMock.Verify(x => x.ConsultarConta(depositoRequestDto.ContaOrigemId), Times.Once);
        _transacaoRepositoryMock.Verify(x => x.AtualizarSaldo(conta.Id, depositoRequestDto.Valor), Times.Once);
        _mapperMock.Verify(x => x.Map<Transacao>(depositoRequestDto), Times.Once);
        _transacaoRepositoryMock.Verify(x => x.SalvarTransacao(transacao), Times.Once);
        _mapperMock.Verify(x => x.Map<TransacaoResponseDto>(transacao), Times.Once);
    }
}
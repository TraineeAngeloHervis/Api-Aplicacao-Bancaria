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

public class TransferenciaServiceTests
{
    private readonly Mock<ITransacaoRepository> _transacaoRepositoryMock;
    private readonly TransferenciaService _transferenciaService;
    private readonly Mock<IMapper> _mapperMock;

    public TransferenciaServiceTests()
    {
        _transacaoRepositoryMock = new Mock<ITransacaoRepository>();
        _mapperMock = new Mock<IMapper>();
        _transferenciaService = new TransferenciaService(_transacaoRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Transferencia_QuandoRealizarTransferencia_DeveRetornarTransacao()
    {
        // Arrange
        var contaOrigem = ContaBuilder.Novo().ComSaldo(1000).Build();
        var contaDestino = ContaBuilder.Novo().ComSaldo(1000).Build();

        var transferenciaRequestDto = TransferenciaRequestDtoBuilder.Novo()
            .ComContaOrigemId(contaOrigem.Id)
            .ComContaDestinoId(contaDestino.Id)
            .ComValor(500)
            .Build();

        var transacao = TransacaoBuilder.Novo().ComTransferenciaRequest(transferenciaRequestDto).Build();
        var transacaoResponseDto = TransacaoResponseDtoBuilder.Novo().ComTransacao(transacao).Build();

        _transacaoRepositoryMock.Setup(x => x
                .ConsultarConta(transferenciaRequestDto.ContaOrigemId))
            .ReturnsAsync(contaOrigem);

        _transacaoRepositoryMock.Setup(x => x
                .ConsultarConta(transferenciaRequestDto.ContaDestinoId))
            .ReturnsAsync(contaDestino);

        _mapperMock.Setup(x => x
            .Map<Transacao>(transferenciaRequestDto)).Returns(transacao);

        _transacaoRepositoryMock.Setup(x => x
                .AtualizarSaldo(contaOrigem.Id, -transferenciaRequestDto.Valor))
            .ReturnsAsync(contaOrigem);

        _transacaoRepositoryMock.Setup(x => x
                .AtualizarSaldo(contaDestino.Id, transferenciaRequestDto.Valor))
            .ReturnsAsync(contaDestino);

        _transacaoRepositoryMock.Setup(x => x
            .SalvarTransacao(transacao)).ReturnsAsync(transacao);
        _mapperMock.Setup(x => x.Map<TransacaoResponseDto>(transacao)).Returns(transacaoResponseDto);


        // Act
        var resultadoEsperado = await _transferenciaService.RealizarTransferencia(transferenciaRequestDto);

        // Assert
        resultadoEsperado.Should().BeEquivalentTo(transacaoResponseDto);
        _mapperMock.Verify(x => x.Map<Transacao>(transferenciaRequestDto), Times.Once);
        _transacaoRepositoryMock.Verify(x => x
            .AtualizarSaldo(transferenciaRequestDto.ContaOrigemId, -transferenciaRequestDto.Valor), Times.Once);
        _transacaoRepositoryMock.Verify(x => x
            .AtualizarSaldo(transferenciaRequestDto.ContaDestinoId, transferenciaRequestDto.Valor), Times.Once);
        _mapperMock.Verify(x => x.Map<TransacaoResponseDto>(transacao), Times.Once);
    }
}
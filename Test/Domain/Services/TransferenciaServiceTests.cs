using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces.Transacoes;
using Domain.Services;
using FluentAssertions;
using Moq;
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
        var transferenciaRequestDto = TransferenciaRequestDtoBuilder.Novo().Build();
        var transacao = TransacaoBuilder.Novo().Build();
        var transacaoResponseDto =
            TransacaoResponseDtoBuilder.Novo().ComTransferenciaRequest(transferenciaRequestDto).Build();

        _mapperMock.Setup(x => x.Map<Transacao>(transferenciaRequestDto)).Returns(transacao);
        await _transferenciaService.RealizarTransferencia(transferenciaRequestDto);
        _transacaoRepositoryMock.Setup(x => x
                .AtualizarSaldo(transferenciaRequestDto.ContaOrigemId, -transferenciaRequestDto.Valor))
            .ReturnsAsync(transacao.ContaOrigem);

        _transacaoRepositoryMock.Setup(x => x
                .AtualizarSaldo(transferenciaRequestDto.ContaDestinoId, transferenciaRequestDto.Valor))
            .ReturnsAsync(transacao.ContaDestino);

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
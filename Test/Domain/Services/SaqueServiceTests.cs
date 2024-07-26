using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces.Transacoes;
using Domain.Services;
using FluentAssertions;
using Moq;
using Test.Crosscutting.Transacoes;

namespace Test.Domain.Services;

public class SaqueServiceTests
{
    private readonly Mock<ITransacaoRepository> _transacaoRepositoryMock;
    private readonly SaqueService _saqueService;
    private readonly Mock<IMapper> _mapperMock;

    public SaqueServiceTests()
    {
        _transacaoRepositoryMock = new Mock<ITransacaoRepository>();
        _mapperMock = new Mock<IMapper>();
        _saqueService = new SaqueService(_transacaoRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Saque_QuandoRealizarSaque_DeveRetornarTransacao()
    {
        // Arrange
        var saqueRequestDto = SaqueRequestDtoBuilder.Novo().Build();
        var transacao = TransacaoBuilder.Novo().Build();
        var transacaoResponseDto = TransacaoResponseDtoBuilder.Novo().ComSaqueRequest(saqueRequestDto).Build();

        _mapperMock.Setup(x => x.Map<Transacao>(saqueRequestDto)).Returns(transacao);
        await _saqueService.RealizarSaque(saqueRequestDto);
        _transacaoRepositoryMock.Setup(x => x
                .AtualizarSaldo(saqueRequestDto.ContaOrigemId, saqueRequestDto.Valor))
            .ReturnsAsync(transacao.ContaOrigem);
        _mapperMock.Setup(x => x.Map<TransacaoResponseDto>(transacao)).Returns(transacaoResponseDto);

        // Act
        var resultadoEsperado = await _saqueService.RealizarSaque(saqueRequestDto);

        // Assert
        resultadoEsperado.Should().BeEquivalentTo(transacaoResponseDto);
        _mapperMock.Verify(x => x.Map<Transacao>(saqueRequestDto), Times.Once);
        _transacaoRepositoryMock.Verify(x => x
            .AtualizarSaldo(saqueRequestDto.ContaOrigemId, saqueRequestDto.Valor), Times.Once);
        _mapperMock.Verify(x => x.Map<TransacaoResponseDto>(transacao), Times.Once);
    }
}
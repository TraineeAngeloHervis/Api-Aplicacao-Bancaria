using AutoMapper;
using Crosscutting.Dto;
using Domain.Interfaces.Transacoes;
using Domain.Services;
using FluentAssertions;
using Moq;
using Test.Crosscutting.Transacoes;

namespace Test.Domain.Services;

public class TransacaoServiceTests
{
    private readonly Mock<ITransacaoRepository> _transacaoRepositoryMock;
    private readonly TransacaoService _transacaoService;
    private readonly Mock<IMapper> _mapperMock;

    public TransacaoServiceTests()
    {
        _transacaoRepositoryMock = new Mock<ITransacaoRepository>();
        _mapperMock = new Mock<IMapper>();
        _transacaoService = new TransacaoService(_transacaoRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Transacao_QuandoConsultarTransacao_DeveRetornarTransacao()
    {
        // Arrange
        var transacaoResponseDto = TransacaoResponseDtoBuilder.Novo().Build();
        var transacao = TransacaoBuilder.Novo().Build();

        _mapperMock.Setup(x => x.Map<TransacaoResponseDto>(transacao)).Returns(transacaoResponseDto);
        await _transacaoService.ConsultarTransacao(transacao.Id);
        _transacaoRepositoryMock.Setup(x => x
                .ConsultarTransacao(transacao.Id))
            .ReturnsAsync(transacao);
        _mapperMock.Setup(x => x.Map<TransacaoResponseDto>(transacao)).Returns(transacaoResponseDto);

        // Act
        var resultadoEsperado = await _transacaoService.ConsultarTransacao(transacaoResponseDto.Id);

        // Assert
        resultadoEsperado.Should().BeEquivalentTo(transacaoResponseDto);
        _mapperMock.Verify(x => x.Map<TransacaoResponseDto>(transacao), Times.Once);
        _transacaoRepositoryMock.Verify(x => x
            .ConsultarTransacao(transacao.Id), Times.Once);
        _mapperMock.Verify(x => x.Map<TransacaoResponseDto>(transacao), Times.Once);
    }
}
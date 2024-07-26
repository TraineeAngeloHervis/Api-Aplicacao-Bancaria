using AutoMapper;
using Crosscutting.Dto;
using Crosscutting.Enums;
using Domain.Entities;
using Domain.Interfaces.Transacoes;
using Domain.Services;
using FluentAssertions;
using Moq;
using Test.Crosscutting.Transacoes;
using Xunit;

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
        var depositoRequestDto = DepositoRequestDtoBuilder.Novo().Build();
        var transacaoResponseDto = TransacaoResponseDtoBuilder.Novo().ComDepositoRequest(depositoRequestDto).Build();
        //criar a transação sem usar o builder
        var transacao = new Transacao
        {
            Id = Guid.NewGuid(),
            Valor = depositoRequestDto.Valor,
            ContaOrigemId = depositoRequestDto.ContaOrigemId,
            TipoTransacao = TipoTransacao.Deposito,
            DataTransacao = DateTime.Now
        };
        

        _mapperMock.Setup(x => x.Map<Transacao>(depositoRequestDto)).Returns(transacao);
        
        _transacaoRepositoryMock.Setup(x => x.AtualizarSaldo(depositoRequestDto.ContaOrigemId, depositoRequestDto.Valor))
            .ReturnsAsync(transacao.ContaOrigem);

        _transacaoRepositoryMock.Setup(x => x.SalvarTransacao(It.IsAny<Transacao>()))
            .ReturnsAsync(transacao);

        _mapperMock.Setup(x => x.Map<TransacaoResponseDto>(transacao)).Returns(transacaoResponseDto);

        // Act
        var resultadoEsperado = await _depositoService.RealizarDeposito(depositoRequestDto);

        // Assert
        resultadoEsperado.Should().BeEquivalentTo(transacaoResponseDto);
        _mapperMock.Verify(x => x.Map<Transacao>(depositoRequestDto), Times.Once);
        _transacaoRepositoryMock.Verify(x => x.AtualizarSaldo(depositoRequestDto.ContaOrigemId, depositoRequestDto.Valor), Times.Once);
        _transacaoRepositoryMock.Verify(x => x.SalvarTransacao(It.IsAny<Transacao>()), Times.Once);
        _mapperMock.Verify(x => x.Map<TransacaoResponseDto>(transacao), Times.Once);
    }
}

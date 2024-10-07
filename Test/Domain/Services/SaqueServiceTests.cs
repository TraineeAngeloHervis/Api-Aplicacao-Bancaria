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
        var conta = ContaBuilder.Novo().ComSaldo(9999).Build();
        var saqueRequestDto = SaqueRequestDtoBuilder.Novo().ComContaOrigemId(conta.Id).ComValor(500).Build();
        var transacao = TransacaoBuilder.Novo().ComSaqueRequest(saqueRequestDto).Build();
        var transacaoResponseDto = TransacaoResponseDtoBuilder.Novo().ComTransacao(transacao).Build();
        
        _transacaoRepositoryMock.Setup(x => x
            .ConsultarConta(saqueRequestDto.ContaOrigemId)).ReturnsAsync(conta);
        
        _transacaoRepositoryMock.Setup(x => x
            .AtualizarSaldo(conta.Id, -saqueRequestDto.Valor)).ReturnsAsync(conta);
        
        _mapperMock.Setup(x => x
            .Map<Transacao>(saqueRequestDto)).Returns(transacao);
        
        _transacaoRepositoryMock.Setup(x => x
            .SalvarTransacao(transacao)).ReturnsAsync(transacao);
        
        _mapperMock.Setup(x => x.Map<TransacaoResponseDto>(transacao)).Returns(transacaoResponseDto);
        
        // Act
        var resultadoEsperado = await _saqueService.RealizarSaque(saqueRequestDto);
        
        // Assert
        resultadoEsperado.Should().BeEquivalentTo(transacaoResponseDto);
        _transacaoRepositoryMock.Verify(x => x.ConsultarConta(saqueRequestDto.ContaOrigemId), Times.Once);
        _transacaoRepositoryMock.Verify(x => x.AtualizarSaldo(conta.Id, -saqueRequestDto.Valor), Times.Once);
        _mapperMock.Verify(x => x.Map<Transacao>(saqueRequestDto), Times.Once);
        _transacaoRepositoryMock.Verify(x => x.SalvarTransacao(transacao), Times.Once);
        _mapperMock.Verify(x => x.Map<TransacaoResponseDto>(transacao), Times.Once);
    }
}
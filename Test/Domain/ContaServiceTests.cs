using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FluentAssertions;
using Moq;
using Test.Crosscutting;

namespace Test.Domain;

public class ContaServiceTests
{
    private readonly Mock<IContaRepository> _contaRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ContaService _contaService;
    
    public ContaServiceTests()
    {
        _contaRepositoryMock = new Mock<IContaRepository>();
        _mapperMock = new Mock<IMapper>();
        _contaService = new ContaService(_contaRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Conta_QuandoCadastrarConta_DeveRetornarConta()
    {
        // Arrange
        var cliente = ClienteBuilder.Novo().Build();
        var contaRequestDto = ContaRequestDtoBuilder.Novo().Build();
        var conta = ContaBuilder.Novo().Build();
        var contaResponseDto = ContaResponseDtoBuilder.Novo().ComContaRequest(contaRequestDto).Build();
        
        _mapperMock.Setup(x => x.Map<Conta>(contaRequestDto)).Returns(conta);
        _contaRepositoryMock.Setup(x => x.CadastrarConta(cliente.Id, conta)).ReturnsAsync(conta);
        _mapperMock.Setup(x => x.Map<ContaResponseDto>(conta)).Returns(contaResponseDto);
        
        // Act
        var resultadoEsperado = await _contaService.CadastrarConta(contaRequestDto, cliente.Id);
        
        // Assert
        resultadoEsperado.Should().BeEquivalentTo(contaResponseDto);
        _mapperMock.Verify(x => x.Map<Conta>(contaRequestDto), Times.Once);
        _contaRepositoryMock.Verify(x => x.CadastrarConta(cliente.Id, conta), Times.Once);
        _mapperMock.Verify(x => x.Map<ContaResponseDto>(conta), Times.Once);
    }
    
    [Fact]
    public async Task Conta_QuandoAtualizarConta_DeveRetornarContaAtualizada()
    {
        // Arrange
        var cliente = ClienteBuilder.Novo().Build();
        var contaRequestDto = ContaRequestDtoBuilder.Novo().Build();
        var conta = ContaBuilder.Novo().Build();
        var contaResponseDto = ContaResponseDtoBuilder.Novo().ComContaRequest(contaRequestDto).Build();
        
        _mapperMock.Setup(x => x.Map<Conta>(contaRequestDto)).Returns(conta);
        _contaRepositoryMock.Setup(x => x.AtualizarConta(cliente.Id, conta, conta.Id)).ReturnsAsync(conta);
        _mapperMock.Setup(x => x.Map<ContaResponseDto>(conta)).Returns(contaResponseDto);
        
        // Act
        var resultadoEsperado = await _contaService.AtualizarConta(cliente.Id, contaRequestDto, conta.Id);
        
        // Assert
        resultadoEsperado.Should().BeEquivalentTo(contaResponseDto);
        _mapperMock.Verify(x => x.Map<Conta>(contaRequestDto), Times.Once);
        _contaRepositoryMock.Verify(x => x.AtualizarConta(cliente.Id, conta, conta.Id), Times.Once);
        _mapperMock.Verify(x => x.Map<ContaResponseDto>(conta), Times.Once);
    }

    [Fact]
    public async Task Conta_QuandoExcluirConta_DeveRetornarTrue()
    {
        // Arrange
        var conta = ContaBuilder.Novo().Build();
        _contaRepositoryMock.Setup(x => x.ConsultarConta(It.IsAny<Guid>())).ReturnsAsync(conta);
        _contaRepositoryMock.Setup(x => x.ExcluirConta(It.IsAny<Guid>())).ReturnsAsync(true);
        
        // Act
        var resultadoEsperado = await _contaService.ExcluirConta(conta.Id);
        
        // Assert
        resultadoEsperado.Should().BeTrue();
        _contaRepositoryMock.Verify(x => x.ConsultarConta(conta.Id), Times.Once);
        _contaRepositoryMock.Verify(x => x.ExcluirConta(conta.Id), Times.Once);
    }
    
    [Fact]
    public async Task Conta_QuandoConsultarConta_DeveRetornarConta()
    {
        // Arrange
        var conta = ContaBuilder.Novo().Build();
        var contaResponseDto = ContaResponseDtoBuilder.Novo().Build();
        
        _contaRepositoryMock.Setup(x => x.ConsultarConta(conta.Id)).ReturnsAsync(conta);
        _mapperMock.Setup(x => x.Map<ContaResponseDto>(conta)).Returns(contaResponseDto);
        
        // Act
        var resultadoEsperado = await _contaService.ConsultarConta(conta.Id);
        
        // Assert
        resultadoEsperado.Should().BeEquivalentTo(contaResponseDto);
        _contaRepositoryMock.Verify(x => x.ConsultarConta(conta.Id), Times.Once);
        _mapperMock.Verify(x => x.Map<ContaResponseDto>(conta), Times.Once);
    }
    
    [Fact]
    public async Task Conta_QuandoListarContas_DeveRetornarContas()
    {
        // Arrange
        var cliente = ClienteBuilder.Novo().Build();
        var contas = new List<ContaResponseDto>()
        {
            ContaResponseDtoBuilder.Novo().Build(),
            ContaResponseDtoBuilder.Novo().Build(),
            ContaResponseDtoBuilder.Novo().Build()
        };
        _contaRepositoryMock.Setup(x => x.ListarContas(cliente.Id)).ReturnsAsync(new List<Conta>());
        _mapperMock.Setup(x => x.Map<IEnumerable<ContaResponseDto>>(It.IsAny<IEnumerable<Conta>>())).Returns(contas);
        
        // Act
        var resultadoEsperado = await _contaService.ListarContas(cliente.Id);
        
        // Assert
        resultadoEsperado.Should().BeEquivalentTo(contas);
        _contaRepositoryMock.Verify(x => x.ListarContas(cliente.Id), Times.Once);
        _mapperMock.Verify(x => x.Map<IEnumerable<ContaResponseDto>>(It.IsAny<IEnumerable<Conta>>()), Times.Once);
    }
}
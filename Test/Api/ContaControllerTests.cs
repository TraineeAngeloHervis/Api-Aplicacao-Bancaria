using Api.Controllers;
using Bogus;
using Crosscutting.Dto;
using Domain.Interfaces;
using Moq;
using Xunit;
using FluentAssertions;

namespace Test.Api;

public class ContaControllerTests
{
    private readonly Mock<IContaService> _contaServiceMock;
    private readonly ContaController _contaController;

    public ContaControllerTests()
    {
        _contaServiceMock = new Mock<IContaService>();
        _contaController = new ContaController(_contaServiceMock.Object);
    }

    [Fact]
    public void Conta_QuandoCadastrarConta_DeveRetornarContaCriada()
    {
        
    }
        
}
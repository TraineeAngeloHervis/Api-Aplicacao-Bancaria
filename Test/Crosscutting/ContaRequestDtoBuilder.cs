using Bogus;
using Crosscutting.Dto;
using Crosscutting.Enums;

namespace Test.Crosscutting;

public class ContaRequestDtoBuilder
{
    private readonly Faker<ContaRequestDto> _faker;

    private ContaRequestDtoBuilder()
    {
        _faker = new Faker<ContaRequestDto>("pt_BR")
            .RuleFor(x => x.ClienteId, f => f.Random.Guid())
            .RuleFor(x => x.SaldoInicial, f => f.Random.Decimal())
            .RuleFor(x => x.TipoConta, f => f.PickRandom<TipoConta>())
            .RuleFor(x => x.DataAbertura, f => f.Date.Past());
    }
    
    public static ContaRequestDtoBuilder Novo()
        => new();

    public ContaRequestDtoBuilder ComClienteId(Guid clienteId)
    {
        _faker.RuleFor(x => x.ClienteId, f => clienteId);
        return this;
    }
    
    public ContaRequestDtoBuilder ComSaldoInicial(decimal saldoInicial)
    {
        _faker.RuleFor(x => x.SaldoInicial, f => saldoInicial);
        return this;
    }
    
    public ContaRequestDtoBuilder ComTipoConta(TipoConta tipoConta)
    {
        _faker.RuleFor(x => x.TipoConta, f => tipoConta);
        return this;
    }
    
    public ContaRequestDtoBuilder ComDataAbertura(DateTime dataAbertura)
    {
        _faker.RuleFor(x => x.DataAbertura, f => dataAbertura);
        return this;
    }
    
    public ContaRequestDto Build()
        => _faker.Generate();
}
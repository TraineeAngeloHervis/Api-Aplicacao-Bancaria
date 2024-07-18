using Bogus;
using Crosscutting.Dto;
using Crosscutting.Enums;

namespace Test.Crosscutting;

public class ContaResponseDtoBuilder
{
    private readonly Faker<ContaResponseDto> _faker;

    private ContaResponseDtoBuilder()
    {
        _faker = new Faker<ContaResponseDto>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.ClienteId, f => f.Random.Guid())
            .RuleFor(x => x.SaldoInicial, f => f.Random.Decimal())
            .RuleFor(x => x.TipoConta, f => f.PickRandom<TipoConta>())
            .RuleFor(x => x.DataAbertura, f => f.Date.Past());
    }
    
    public static ContaResponseDtoBuilder Novo()
        => new();

    public ContaResponseDtoBuilder ComId(Guid id)
    {
        _faker.RuleFor(x => x.Id, f => id);
        return this;
    }
    
    public ContaResponseDtoBuilder ComClienteId(Guid clienteId)
    {
        _faker.RuleFor(x => x.ClienteId, f => clienteId);
        return this;
    }
    
    public ContaResponseDtoBuilder ComSaldoInicial(decimal saldoInicial)
    {
        _faker.RuleFor(x => x.SaldoInicial, f => saldoInicial);
        return this;
    }
    
    public ContaResponseDtoBuilder ComTipoConta(TipoConta tipoConta)
    {
        _faker.RuleFor(x => x.TipoConta, f => tipoConta);
        return this;
    }
    
    public ContaResponseDtoBuilder ComDataAbertura(DateTime dataAbertura)
    {
        _faker.RuleFor(x => x.DataAbertura, f => dataAbertura);
        return this;
    }
    
    public ContaResponseDto Build()
        => _faker.Generate();
}
using Bogus;
using Crosscutting.Dto;

namespace Test.Crosscutting.Transacoes;

public class DepositoRequestDtoBuilder
{
    private readonly Faker<DepositoRequestDto> _faker;
    
    public DepositoRequestDtoBuilder()
    {
        _faker = new Faker<DepositoRequestDto>("pt_BR")
            .RuleFor(x => x.Valor, f => f.Random.Decimal(0, 100))
            .RuleFor(x => x.ContaOrigemId, f => f.Random.Guid());
    }
    
    public static DepositoRequestDtoBuilder Novo()
        => new();

    public DepositoRequestDtoBuilder ComDepositoRequest(DepositoRequestDto depositoRequestDto)
    {
        _faker.RuleFor(x => x.Valor, f => depositoRequestDto.Valor);
        _faker.RuleFor(x => x.ContaOrigemId, f => depositoRequestDto.ContaOrigemId);
        return this;
    }
    
    public DepositoRequestDtoBuilder ComValor(decimal valor)
    {
        _faker.RuleFor(x => x.Valor, f => valor);
        return this;
    }
    
    public DepositoRequestDtoBuilder ComContaOrigemId(Guid contaOrigemId)
    {
        _faker.RuleFor(x => x.ContaOrigemId, f => contaOrigemId);
        return this;
    }
    
    public DepositoRequestDto Build()
        => _faker.Generate();
}
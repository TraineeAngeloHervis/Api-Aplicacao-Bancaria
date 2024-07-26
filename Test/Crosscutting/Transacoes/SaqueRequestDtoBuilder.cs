using Bogus;
using Crosscutting.Dto;
using Crosscutting.Enums;

namespace Test.Crosscutting.Transacoes;

public class SaqueRequestDtoBuilder
{
    private readonly Faker<SaqueRequestDto> _faker;
    
    public SaqueRequestDtoBuilder()
    {
        _faker = new Faker<SaqueRequestDto>("pt_BR")
            .RuleFor(x => x.Valor, f => f.Random.Decimal(1, 100))
            .RuleFor(x => x.ContaOrigemId, f => f.Random.Guid())
            .RuleFor(x => x.TipoTransacao, f => TipoTransacao.Saque);
    }
    
    public static SaqueRequestDtoBuilder Novo()
        => new();

    public SaqueRequestDtoBuilder ComSaqueRequest(SaqueRequestDto saqueRequestDto)
    {
        _faker.RuleFor(x => x.Valor, f => saqueRequestDto.Valor);
        _faker.RuleFor(x => x.ContaOrigemId, f => saqueRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.TipoTransacao, f => saqueRequestDto.TipoTransacao);
        return this;
    }
    
    public SaqueRequestDtoBuilder ComValor(decimal valor)
    {
        _faker.RuleFor(x => x.Valor, f => valor);
        return this;
    }
    
    public SaqueRequestDtoBuilder ComContaOrigemId(Guid contaOrigemId)
    {
        _faker.RuleFor(x => x.ContaOrigemId, f => contaOrigemId);
        return this;
    }
    
    public SaqueRequestDto Build()
        => _faker.Generate();
}